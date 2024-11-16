using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.ServiceInterfaces
{
    public interface IQuizService : IGenericServiceAsync<QuizDTO>
    {
        Task<IEnumerable<QuizDTO>> GetQuizzesByMaterialAsync(int materialId);
        Task<IEnumerable<QuizDTO>> GetQuizzesByWriterAsync(int writerId);
        //Task<IEnumerable<QuizDTO>> GetTopQuizzesAsync(int materialId, int count);
        Task<IEnumerable<QuizDTO>> GetQuizzesByStatusAsync(bool isDraft);
    }
}
