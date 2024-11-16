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
    public class UserRepository : GenericRepositoryAsync<User>, IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(QuizAppDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<User>();
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetTrustedWritersAsync()
        {
            return await _users.AsNoTracking()
                .Where(u => u.IsTrusted)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetDoctorsAsync()
        {
            return await _users.AsNoTracking()
                .Where(u => u.IsDoctor)
                .ToListAsync();
        }

        public async Task<int?> GetUserIdByEmailAsync(string email)
        {
            var user = await _users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email);

            return user?.UserId;
        }

    }
}
