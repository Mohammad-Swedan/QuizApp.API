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
    public class SpecializationRepository : GenericRepositoryAsync<Specialization>, ISpecializationRepository
    {
        private readonly DbSet<Specialization> _specializations;

        public SpecializationRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            _specializations = dbContext.Set<Specialization>();
        }

        public async Task<IEnumerable<Specialization>> GetSpecializationsByCollegeAsync(int collegeId)
        {
            return await _specializations.AsNoTracking()
                .Where(s => s.CollegeId == collegeId)
                .ToListAsync();
        }

    }
}
