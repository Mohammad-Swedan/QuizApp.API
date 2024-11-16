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
    public class QuestionRepository : GenericRepositoryAsync<Question>, IQuestionRepository
    {
        private readonly DbSet<Question> _questions;

        public QuestionRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            _questions = dbContext.Set<Question>();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByQuizAsync(int quizId)
        {
            return await _questions.AsNoTracking()
                .Where(q => q.QuizId == quizId)
                .ToListAsync();
        }

    }
}
