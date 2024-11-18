using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.DAL.DTOs;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizForAndroid.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling like and dislike actions on quizzes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LikeDislikeController : ControllerBase
    {
        private readonly IQuizLikesDislikesService _likeDislikeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LikeDislikeController"/> class.
        /// </summary>
        /// <param name="likeDislikeService">Service for like/dislike operations.</param>
        public LikeDislikeController(IQuizLikesDislikesService likeDislikeService)
        {
            _likeDislikeService = likeDislikeService;
        }

        /// <summary>
        /// Adds or removes a like or dislike for a quiz by the authenticated user.
        /// </summary>
        /// <param name="model">The like/dislike data.</param>
        /// <returns>A success message upon successful processing.</returns>
        /// <response code="200">If the action is processed successfully.</response>
        /// <response code="400">If the request is invalid or processing fails.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpPost(Name = "AddLikeOrDislike")]
        [Authorize]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 401)]
        public async Task<IActionResult> AddLikeOrDislike([FromBody] QuizLikesDislikesDTO model)
        {
            /// <summary>
            /// Adds or removes a like or dislike for a quiz by the authenticated user.
            /// </summary>
            /// <param name="model">The like/dislike data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Get the user ID from the claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "User is not authenticated." });
                }

                if (!int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized(new { message = "Invalid user ID in token." });
                }

                await _likeDislikeService.AddLikeOrDislikeAsync(model.QuizID, userId, model.IsLike);

                return Ok(new { message = "Like/dislike action processed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
