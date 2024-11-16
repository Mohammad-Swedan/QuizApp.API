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

        public QuizLikesDislikesRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            _likesDislikes = dbContext.Set<QuizLikesDislike>();
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

    }
}
