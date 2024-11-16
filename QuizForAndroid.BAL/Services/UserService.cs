using AutoMapper;
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
    public class UserService //: GenericService<User, UserDto>, IUserService
    {
        //private readonly IUserRepository _userRepository;
        //private readonly IMapper _mapper;
        //private readonly ITokenService _tokenService;
        //private readonly IUserRoleService _userRoleService;

        //public UserService(IUserRepository userRepository, IMapper mapper, IUserRoleService userRoleService, ITokenService tokenService)
        //    : base(userRepository, mapper)
        //{
        //    _userRepository = userRepository;
        //    _mapper = mapper;
        //    _userRoleService = userRoleService;
        //    _tokenService = tokenService;
        //}

        //public async Task<UserDTO> FindByEmailAsync(string email)
        //{
        //    var user = await _userRepository.FindByEmailAsync(email);
        //    return _mapper.Map<UserDTO>(user);
        //}

        //public async Task<UserPublicDto> RegisterUserAsync(RegisterDTO model)
        //{
        //    var existingUser = await _userRepository.FindByEmailAsync(model.Email);
        //    if (existingUser != null)
        //        throw new ApplicationException($"User with email '{model.Email}' already exists.");

        //    var user = _mapper.Map<User>(model);

        //    PasswordHelper.CreatePasswordHash(model.Password, out byte[] passwordSalt, out byte[] passwordHash);
        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;

        //    // Additional user initialization
        //    user.FirstName = model.FirstName;
        //    user.LastName = model.LastName;
        //    user.IsTrusted = false;
        //    user.IsDoctor = false;
            
        //    //TODO : Add created date
        //    //user.CreatedDate = DateTime.UtcNow;

        //    await _userRepository.AddAsync(user);
        //    await _userRoleService.AssignRoleToUserAsync("User", user);

        //    //change UserPublic DTO
        //    return _mapper.Map<UserPublicDto>(user);
        //}

        //public async Task<string> AuthenticateUserAsync(LoginDTO model)
        //{
        //    var user = await _userRepository.FindByEmailAsync(model.Email);
        //    if (user == null)
        //        throw new UnauthorizedAccessException("Invalid email or password.");

        //    if (!PasswordHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
        //        throw new UnauthorizedAccessException("Invalid email or password.");

        //    // Fetch user roles
        //    var roles = await _userRoleService.GetUserRolesAsync(user.UserID);
        //    var roleNames = roles.Select(r => r.RoleName).ToList();

        //    // Generate JWT token
        //    var token = await _tokenService.GenerateJwtTokenAsync(user, roleNames);
        //    return token;
        //}
    }
}
