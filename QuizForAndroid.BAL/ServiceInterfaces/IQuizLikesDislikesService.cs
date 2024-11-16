using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.ServiceInterfaces
{
    public interface IQuizLikesDislikesService : IGenericServiceAsync<QuizLikesDislikesDTO>
    {
        Task<int> GetLikesCountAsync(int quizId);
        Task<int> GetDislikesCountAsync(int quizId);
        Task<QuizLikesDislikesDTO> GetUserLikeDislikeAsync(int userId, int quizId);
    }
}
