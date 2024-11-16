using QuizForAndroid.DAL.Entities;
using QuizForAndroid.DAL.GenericBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.Abstracts
{
    public interface IQuizRepository : IGenericRepositoryAsync<Quiz>
    {
        Task<IEnumerable<Quiz>> GetQuizzesByMaterialAsync(int materialId);
        Task<IEnumerable<Quiz>> GetQuizzesByWriterAsync(int writerId);
        //Task<IEnumerable<Quiz>> GetTopQuizzesAsync(int materialId, int count);
        Task<IEnumerable<Quiz>> GetQuizzesByStatusAsync(bool isDraft);
        //Get Draft Quizzes 
    }
}
