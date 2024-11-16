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
    public class MaterialRepository : GenericRepositoryAsync<Material>, IMaterialRepository
    {
        private readonly DbSet<Material> _materials;

        public MaterialRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            _materials = dbContext.Set<Material>();
        }

        public async Task<IEnumerable<Material>> GetMaterialsByCollegeAsync(int collegeId)
        {
            return await _materials.AsNoTracking()
                .Where(m => m.CollegeId == collegeId)
                .ToListAsync();
        }

    }
}
