using QuizForAndroid.DAL.Entities;
using QuizForAndroid.DAL.GenericBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.Abstracts
{
    public interface IQuizLikesDislikesRepository : IGenericRepositoryAsync<QuizLikesDislike>
    {
        Task<int> GetLikesCountAsync(int quizId);
        Task<int> GetDislikesCountAsync(int quizId);
        Task<QuizLikesDislike> GetUserLikeDislikeAsync(int userId, int quizId);
        public Task AddLikeOrDislikeAsync(int quizId, int userId, bool likeOrDislike);
        public Task<short> GetLikeStatusAsync(int quizId, int userId);

    }
}
