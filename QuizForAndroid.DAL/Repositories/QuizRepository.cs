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
    public class QuizRepository : GenericRepositoryAsync<Quiz>, IQuizRepository
    {
        private readonly DbSet<Quiz> _quizzes;

        public QuizRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            _quizzes = dbContext.Set<Quiz>();
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByMaterialAsync(int materialId)
        {
            return await _quizzes.AsNoTracking()
                .Where(q => q.MaterialId == materialId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByWriterAsync(int writerId)
        {
            return await _quizzes.AsNoTracking()
                .Where(q => q.WriterId == writerId)
                .ToListAsync();
        }

        //TODO:
        //change later - add logic to get top quizzes (depending on several factors and filters)
        public async Task<IEnumerable<Quiz>> GetTopQuizzesAsync(int materialId, int count)
        {
            return await _quizzes.AsNoTracking()
                .Where(q => q.MaterialId == materialId && !q.IsDraft)
                .OrderByDescending(q => q.CreatedDate) 
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByStatusAsync(bool isDraft)
        {
            return await _quizzes.AsNoTracking()
                .Where(q => q.IsDraft == isDraft)
                .ToListAsync();
        }

    }
}
