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
    public class WriterApplicationRepository : GenericRepositoryAsync<WriterApplication>, IWriterApplicationRepository
    {
        private readonly DbSet<WriterApplication> _applications;

        // add it later inside UOW
        IUserRepository _userRepository;

        public WriterApplicationRepository(QuizAppDbContext dbContext,IUserRepository userRepository) : base(dbContext)
        {
            _applications = dbContext.Set<WriterApplication>();
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<WriterApplication>> GetPendingApplicationsAsync()
        {
            return await _applications.AsNoTracking()
                .Where(wa => wa.Status == "Pending")
                .ToListAsync();
        }

        public async Task<IEnumerable<WriterApplication>> GetApplicationsByUserEmailAsync(string email)
        {
            
            int? userId = await _userRepository.GetUserIdByEmailAsync(email);

            return await _applications.AsNoTracking()
                .Where(wa => wa.UserId == userId)
                .ToListAsync();
        }
    }
}
