using QuizForAndroid.DAL.Entities;
using QuizForAndroid.DAL.GenericBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.Abstracts
{
    public interface IUserRepository : IGenericRepositoryAsync<User>
    {
        Task<User> FindByEmailAsync(string email);
        Task<IEnumerable<User>> GetTrustedWritersAsync();
        Task<IEnumerable<User>> GetDoctorsAsync();
        Task<int?> GetUserIdByEmailAsync(string email);
    }
}
