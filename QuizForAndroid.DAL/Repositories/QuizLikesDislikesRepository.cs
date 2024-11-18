using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuizForAndroid.DAL.Abstracts;
using QuizForAndroid.DAL.Contexts;
using QuizForAndroid.DAL.Entities;
using QuizForAndroid.DAL.GenericBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.Repositories
{
    public class QuizLikesDislikesRepository : GenericRepositoryAsync<QuizLikesDislike>, IQuizLikesDislikesRepository
    {
        private readonly DbSet<QuizLikesDislike> _likesDislikes;
        private readonly DbContext _context;

        public QuizLikesDislikesRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            _likesDislikes = dbContext.Set<QuizLikesDislike>();
            _context = dbContext;
        }

        public async Task<int> GetLikesCountAsync(int quizId)
        {
            return await _likesDislikes.AsNoTracking()
                .CountAsync(ld => ld.QuizId == quizId && ld.IsLike);
        }

        public async Task<int> GetDislikesCountAsync(int quizId)
        {
            return await _likesDislikes.AsNoTracking()
                .CountAsync(ld => ld.QuizId == quizId && !ld.IsLike);
        }

        public async Task<QuizLikesDislike> GetUserLikeDislikeAsync(int userId, int quizId)
        {
            return await _likesDislikes.AsNoTracking()
                .FirstOrDefaultAsync(ld => ld.UserId == userId && ld.QuizId == quizId);
        }

        public async Task AddLikeOrDislikeAsync(int quizId, int userId, bool likeOrDislike)
        {
            var quizIdParam = new SqlParameter("@QuizId", quizId);
            var userIdParam = new SqlParameter("@UserId", userId);
            var likeOrDislikeParam = new SqlParameter("@LikeOrDislike", likeOrDislike);

            await  _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_AddLikeOrDislike @QuizId, @UserId, @LikeOrDislike",
                quizIdParam, userIdParam, likeOrDislikeParam);
        }

        public async Task<short> GetLikeStatusAsync(int quizId, int userId)
        {
            
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SP_GetLikeOrDislikeStatus";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@QuizId", quizId));
                command.Parameters.Add(new SqlParameter("@UserId", userId));

                if (command.Connection.State != System.Data.ConnectionState.Open)
                {
                    await command.Connection.OpenAsync();
                }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var likeStatusObj = reader["LikeStatus"];
                        if (likeStatusObj == DBNull.Value || likeStatusObj == null)
                        {
                            return 0; // 0: No action (neutral)
                        }
                        else
                        {
                            return Convert.ToInt16(likeStatusObj); // 1 for like, 0 for dislike
                        }
                    }
                    else
                    {
                        return 0; // 0: No action (neutral)
                    }
                }
            }
        }



    }
}
