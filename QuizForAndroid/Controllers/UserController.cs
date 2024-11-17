using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.DAL.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using QuizForAndroid.DAL.Migrations;

namespace QuizForAndroid.API.Controllers
{
    /// <summary>
    /// Controller responsible for user management operations such as retrieving and updating user information.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">Service for user-related operations.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The user details.</returns>
        /// <response code="200">Returns the user details.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpGet("by-email/{email}", Name = "GetUserByEmail")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize] // Requires authentication
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            /// <summary>
            /// Retrieves a user by email.
            /// </summary>
            /// <param name="email">Email of the user.</param>
            /// <returns>ActionResult with user data.</returns>
            try
            {
                var user = await _userService.FindByEmailAsync(email);
                if (user == null)
                    return NotFound(new { message = $"User with email '{email}' not found." });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates the user's information, including first name, last name, email, and password.
        /// To change the password, the old password must be provided for verification.
        /// </summary>
        /// <param name="model">The updated user information.</param>
        /// <returns>A success message upon successful update.</returns>
        /// <response code="200">If the update is successful.</response>
        /// <response code="400">If the update fails due to invalid input or other errors.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpPut("edit", Name = "EditUserInfo")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        [Authorize] // Requires authentication
        public async Task<IActionResult> EditUserInfo([FromBody] EditUserDTO model)
        {
            /// <summary>
            /// Edits user information.
            /// </summary>
            /// <param name="model">Updated user data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                // Get the current user's ID from the JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return Unauthorized(new { message = "User ID not found in token." });

                int userId = int.Parse(userIdClaim.Value);

                var result = await _userService.UpdateAsync(userId, model); // edit this (pram of update async mis match - send model)
                if (result)
                    return Ok(new { message = "User information updated successfully." });
                else
                    return BadRequest(new { message = "Failed to update user information." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
