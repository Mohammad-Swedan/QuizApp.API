using Microsoft.EntityFrameworkCore.Storage;
using QuizForAndroid.DAL.Abstracts;
using QuizForAndroid.DAL.Contexts;
using QuizForAndroid.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork ,IDisposable
    {
        private readonly QuizAppDbContext _dbContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(QuizAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IUserRepository _users;
        public IUserRepository Users => _users ??= new UserRepository(_dbContext);

        private IRoleRepository _roles;
        public IRoleRepository Roles => _roles ??= new RoleRepository(_dbContext);

        private IUniversityRepository _universities;
        public IUniversityRepository Universities => _universities ??= new UniversityRepository(_dbContext);

        private ICollegeRepository _colleges;
        public ICollegeRepository Colleges => _colleges ??= new CollegeRepository(_dbContext);

        private ISpecializationRepository _specializations;
        public ISpecializationRepository Specializations => _specializations ??= new SpecializationRepository(_dbContext);

        private IMaterialRepository _materials;
        public IMaterialRepository Materials => _materials ??= new MaterialRepository(_dbContext);

        private IQuizRepository _quizzes;
        public IQuizRepository Quizzes => _quizzes ??= new QuizRepository(_dbContext);

        private IQuestionRepository _question;
        public IQuestionRepository Questions => _question ??= new QuestionRepository(_dbContext);

        private IChoiceRepository _choices;
        public IChoiceRepository Choices => _choices ??= new ChoiceRepository(_dbContext);

        private IQuizLikesDislikesRepository _quizLikesDislikes;
        public IQuizLikesDislikesRepository QuizLikesDislikes => _quizLikesDislikes ??= new QuizLikesDislikesRepository(_dbContext);

        private IWriterApplicationRepository _writerApplication;
        public IWriterApplicationRepository WriterApplications => _writerApplication ??= new WriterApplicationRepository(_dbContext,_users);

      
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.SaveChangesAsync();
            await _transaction.CommitAsync();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _transaction?.Dispose();
        }
    }
}
