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
    public class CollegeRepository : GenericRepositoryAsync<College>, ICollegeRepository
    {
        private readonly DbSet<College> _colleges;

        public CollegeRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            _colleges = dbContext.Set<College>();
        }

        public async Task<IEnumerable<College>> GetCollegesByUniversityAsync(int universityId)
        {
            return await _colleges.AsNoTracking()
                .Where(c => c.UniversityId == universityId)
                .ToListAsync();
        }

    }
}
