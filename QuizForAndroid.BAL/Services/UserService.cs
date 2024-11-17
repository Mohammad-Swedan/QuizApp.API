using AutoMapper;
using eCampus_Prototype.BLL.Helpers;
using eCampus_Prototype.BLL.ServiceInterfaces;
using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.DAL.Abstracts;
using QuizForAndroid.DAL.DTOs;
using QuizForAndroid.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.BLL.Services
{
    public class UserService : GenericServiceAsync<User, UserDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IRoleService _roleService;

        public UserService(IUserRepository userRepository, IMapper mapper, IRoleService roleService, ITokenService tokenService)
            : base(userRepository, mapper)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<AddUserDTO> RegisterUserAsync(RegisterDTO model)
        {
            var existingUser = await _userRepository.FindByEmailAsync(model.Email);
            if (existingUser != null)
                throw new ApplicationException($"User with email '{model.Email}' already exists.");

            var user = _mapper.Map<User>(model);

            PasswordHelper.CreatePasswordHash(model.Password, out byte[] passwordSalt, out byte[] passwordHash);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // validate password later .........

            // edite later
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsTrusted = false;
            user.IsDoctor = false;

            //user.FailedLoginAttempts = 0;
            //TODO : add created date
            //user.CreatedDate = DateTime.UtcNow;
            user.RoleId = 1; // 1 : User (2 : Writer , 3 : Admin , 4 : SuperAdmin)

            await _userRepository.AddAsync(user);


            return  _mapper.Map<AddUserDTO>(await _userRepository.FindByEmailAsync(user.Email));
        }

        public async Task<string> AuthenticateUserAsync(LoginDTO model)
        {
            var user = await _userRepository.FindByEmailAsync(model.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            if (!PasswordHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedAccessException("Invalid email or password.");

            // Fetch user roles
            int roleId = user.RoleId;
            List<string> roleNames = new List<string>();

            switch(roleId)
            {
                case 4:
                    roleNames.Add("SuperAdmin");
                    goto case 3;
                case 3:
                    roleNames.Add("Admin");
                    goto case 2;
                case 2:
                    roleNames.Add("Writer");
                    goto case 1;
                case 1:
                    roleNames.Add("User");
                    break;
            }

            // Generate JWT token using TokenService
            var token = await _tokenService.GenerateJwtTokenAsync(user, roleNames);
            return token;
        }

        public Task<User> FindByEmailAsync(string email)
        {
            var result = _userRepository.FindByEmailAsync(email);
            return result!;
        }

        public async Task AssignRoleAsync(User user,int roleId)
        {
            if ((roleId > 3 || roleId < 1) || (user.RoleId == roleId))
                return;

            var role = await _roleService.GetByIdAsync(roleId);

            user.RoleId = role.RoleID;
            await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> UpdateAsync(int userId, EditUserDTO model)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (!PasswordHelper.VerifyPasswordHash(model.OldPassword, user.PasswordHash, user.PasswordSalt))
                return false;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            PasswordHelper.CreatePasswordHash(model.NewPassword, out byte[] passwordSalt, out byte[] passwordHash);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.UpdateAsync(user);

            return true;
        }
    }
}
