using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.ServiceInterfaces
{
    public interface IUserService : IGenericServiceAsync<UserDTO> 
    {
        Task<GetUserDTO> FindByEmailAsync(string email);
        Task<AddUserDTO> RegisterUserAsync(RegisterDTO model);
        Task<string> AuthenticateUserAsync(LoginDTO model);
    }
}
