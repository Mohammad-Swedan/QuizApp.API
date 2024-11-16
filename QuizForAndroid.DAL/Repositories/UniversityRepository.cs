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
    public class UniversityRepository : GenericRepositoryAsync<University>, IUniversityRepository
    {
        private readonly DbSet<University> _universes;
        public UniversityRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            dbContext.Set<University>();
        }

    }
}
