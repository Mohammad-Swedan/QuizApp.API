using QuizForAndroid.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IUniversityRepository Universities { get; }
        ICollegeRepository Colleges { get; }
        ISpecializationRepository Specializations { get; }
        IMaterialRepository Materials { get; }
        IQuizRepository Quizzes { get; }
        IQuestionRepository Questions { get; }
        IChoiceRepository Choices { get; }
        IQuizLikesDislikesRepository QuizLikesDislikes { get; }
        IWriterApplicationRepository WriterApplications { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}
