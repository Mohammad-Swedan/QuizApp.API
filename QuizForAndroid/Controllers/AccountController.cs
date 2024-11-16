using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.DAL.DTOs;

namespace QuizForAndroid.API.Controllers
{
    /// <summary>
    /// Controller responsible for user account management, including registration, login, and role assignments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        //private readonly IUserRoleService _userRoleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userService">Service for user-related operations.</param>
        /// <param name="roleService">Service for role-related operations.</param>
        /// <param name="userRoleService">Service for user-role assignments.</param>
        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
            //_userRoleService = userRoleService;
        }

        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <param name="model">The registration details of the user.</param>
        /// <returns>A newly created user object with a success message.</returns>
        /// <response code="201">Returns the newly created user.</response>
        /// <response code="400">If the registration fails due to invalid input or other errors.</response>
        [HttpPost("register", Name = "RegisterUser")]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            /// <summary>
            /// Registers a new user.
            /// </summary>
            /// <param name="model">Registration data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var user = await _userService.RegisterUserAsync(model);

                if (user != null)
                {
                    // Assuming you have a GetUserById endpoint to retrieve user details
                    return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, new { message = "User registered successfully", User = user });
                }
                else
                {
                    return BadRequest(new { message = "Failed to register user" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Authenticates a user with the provided login credentials and returns a JWT token upon successful authentication.
        /// </summary>
        /// <param name="model">The login credentials of the user.</param>
        /// <returns>A JWT token if authentication is successful.</returns>
        /// <response code="200">Returns the JWT token.</response>
        /// <response code="401">If authentication fails due to invalid credentials.</response>
        [HttpPost("login", Name = "LoginUser")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            /// <summary>
            /// Logs in a user.
            /// </summary>
            /// <param name="model">Login data.</param>
            /// <returns>ActionResult with JWT token.</returns>
            try
            {
                var token = await _userService.AuthenticateUserAsync(model);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Assigns a specified role to a user identified by their email.
        /// </summary>
        /// <param name="model">The role assignment details, including user email and role name.</param>
        /// <returns>A success message upon successful role assignment.</returns>
        /// <response code="200">If the role is assigned successfully.</response>
        /// <response code="400">If the role assignment fails due to invalid input or other errors.</response>
        /// <response code="404">If the user with the specified email is not found.</response>
        [HttpPost("assign-role", Name = "AssignRoleToUser")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "SuperAdmin")] // Uncomment to enforce authorization
        public async Task<IActionResult> AssignRole(AssignRoleDTO model)
        {
            /// <summary>
            /// Assigns a role to a user.
            /// </summary>
            /// <param name="model">Role assignment data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var user = await _userService.FindByEmailAsync(model.Email);

                if (user == null)
                    return NotFound(new { Message = $"User with email: {model.Email} not found!" });

                await _userService.AssignRoleAsync(user,model.RoleId);
                return Ok(new { message = "Role assigned successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user details.</returns>
        /// <response code="200">Returns the user details.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "SuperAdmin, Admin")] // Example authorization
        public async Task<IActionResult> GetUserById(int id)
        {
            /// <summary>
            /// Retrieves a user by ID.
            /// </summary>
            /// <param name="id">User ID.</param>
            /// <returns>ActionResult with user data.</returns>
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = $"User with ID: {id} not found!" });

            return Ok(user);
        }

        // Uncomment and implement if you need to add roles dynamically
        /*
        /// <summary>
        /// Adds a new role to the system.
        /// </summary>
        /// <param name="roleName">The name of the role to add.</param>
        /// <returns>A success message upon successful role addition.</returns>
        /// <response code="200">If the role is added successfully.</response>
        /// <response code="400">If the role addition fails due to invalid input or other errors.</response>
        [HttpPost("add-role", Name = "AddRole")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddRole([FromBody] string roleName)
        {
            /// <summary>
            /// Adds a new role.
            /// </summary>
            /// <param name="roleName">Name of the role to add.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                RoleDto role = new RoleDto { RoleName = roleName };
                await _roleService.AddAsync(role);
                return Ok(new { message = "Role added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        */
    }
}
