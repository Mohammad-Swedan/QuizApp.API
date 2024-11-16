using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.DAL.DTOs;
using QuizForAndroid.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.ServiceInterfaces
{
    public interface IUserService : IGenericServiceAsync<UserDTO> 
    {
        Task<User> FindByEmailAsync(string email);
        Task<AddUserDTO> RegisterUserAsync(RegisterDTO model);
        Task<string> AuthenticateUserAsync(LoginDTO model);
        Task AssignRoleAsync(User user,int roleId);
    }
}
