using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.DAL.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;

namespace QuizForAndroid.API.Controllers
{
    /// <summary>
    /// Controller responsible for quiz management operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuizController"/> class.
        /// </summary>
        /// <param name="quizService">Service for quiz-related operations.</param>
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        /// <summary>
        /// Retrieves all quizzes.
        /// </summary>
        /// <returns>A list of quizzes.</returns>
        /// <response code="200">Returns the list of quizzes.</response>
        [HttpGet(Name = "GetAllQuizzes")]
        [ProducesResponseType(typeof(IEnumerable<QuizDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllQuizzes()
        {
            /// <summary>
            /// Retrieves all quizzes.
            /// </summary>
            /// <returns>ActionResult with list of quizzes.</returns>
            try
            {
                var quizzes = await _quizService.GetAllAsync();
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a quiz by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the quiz.</param>
        /// <returns>The quiz details.</returns>
        /// <response code="200">Returns the quiz details.</response>
        /// <response code="404">If the quiz is not found.</response>
        [HttpGet("{id}", Name = "GetQuizById")]
        [ProducesResponseType(typeof(QuizDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuizById(int id)
        {
            /// <summary>
            /// Retrieves a quiz by ID.
            /// </summary>
            /// <param name="id">Quiz ID.</param>
            /// <returns>ActionResult with quiz data.</returns>
            try
            {
                var quiz = await _quizService.GetByIdAsync(id);
                if (quiz == null)
                    return NotFound(new { message = $"Quiz with ID {id} not found." });

                return Ok(quiz);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new quiz.
        /// </summary>
        /// <param name="model">The quiz details.</param>
        /// <returns>The created quiz.</returns>
        /// <response code="201">Returns the created quiz.</response>
        /// <response code="400">If the creation fails due to invalid input or other errors.</response>
        [HttpPost(Name = "CreateQuiz")]
        [ProducesResponseType(typeof(QuizDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, SuperAdmin, Writer")]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizDTO model)
        {
            /// <summary>
            /// Creates a new quiz.
            /// </summary>
            /// <param name="model">Quiz data.</param>
            /// <returns>ActionResult with created quiz data.</returns>
            try
            {
                var quiz = await _quizService.AddAsync(model);
                return CreatedAtAction(nameof(GetQuizById), new { id = quiz.QuizID }, quiz);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing quiz.
        /// </summary>
        /// <param name="id">The unique identifier of the quiz to update.</param>
        /// <param name="model">The updated quiz details.</param>
        /// <returns>A success message upon successful update.</returns>
        /// <response code="200">If the update is successful.</response>
        /// <response code="400">If the update fails due to invalid input or other errors.</response>
        /// <response code="404">If the quiz is not found.</response>
        [HttpPut("{id}", Name = "UpdateQuiz")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, SuperAdmin, Writer")]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizDTO model)
        {
            /// <summary>
            /// Updates a quiz.
            /// </summary>
            /// <param name="id">Quiz ID.</param>
            /// <param name="model">Updated quiz data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var quiz = await _quizService.GetByIdAsync(id);
                if (quiz == null)
                    return NotFound(new { message = $"Quiz with ID {id} not found." });

                model.QuizID = id;
                await _quizService.UpdateAsync(model);
                return Ok(new { message = "Quiz updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a quiz by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the quiz to delete.</param>
        /// <returns>A success message upon successful deletion.</returns>
        /// <response code="200">If the deletion is successful.</response>
        /// <response code="404">If the quiz is not found.</response>
        [HttpDelete("{id}", Name = "DeleteQuiz")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, SuperAdmin, Writer")] // make sure that the quiz belong to the wirter before delete
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            /// <summary>
            /// Deletes a quiz.
            /// </summary>
            /// <param name="id">Quiz ID.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var quiz = await _quizService.GetByIdAsync(id);
                if (quiz == null)
                    return NotFound(new { message = $"Quiz with ID {id} not found." });

                await _quizService.DeleteAsync(id);
                return Ok(new { message = "Quiz deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("full-quiz", Name = "AddFullQuiz")]
        [ProducesResponseType(typeof(FullQuizDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, SuperAdmin, Writer")]
        public async Task<IActionResult> AddFullQuiz([FromBody] FullQuizDTO model)
        {
            /// <summary>
            /// Adds a full quiz with its questions and choices.
            /// </summary>
            /// <param name="model">The full quiz data.</param>
            /// <returns>ActionResult with created full quiz data.</returns>
            try
            {
                var quiz = await _quizService.AddFullQuizAsync(model);
                return CreatedAtAction(nameof(GetFullQuiz), new { id = quiz.Quiz.QuizID }, quiz);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("full-quiz/{id}", Name = "GetFullQuiz")]
        [ProducesResponseType(typeof(FullQuizDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFullQuiz(int id)
        {
            /// <summary>
            /// Retrieves a full quiz by its unique identifier, including its questions and choices.
            /// </summary>
            /// <param name="id">The unique identifier of the quiz.</param>
            /// <returns>ActionResult with full quiz data.</returns>
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                    return Unauthorized(new { message = "User ID not found in token." });

                int userId = int.Parse(userIdClaim.Value);

                var fullQuiz = await _quizService.GetFullQuizByIdAsync(id,userId);
                if (fullQuiz == null)
                    return NotFound(new { message = $"Quiz with ID {id} not found." });

                return Ok(fullQuiz);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        /// <summary>
        /// Get quizzes by college id
        /// </summary>
        /// <param name="collegeId">college id</param>
        /// <returns> list of quizzes </returns>
        //[HttpGet("by-college/{collegeId}", Name = "GetQuizzesByCollege")]
        //[ProducesResponseType(typeof(IEnumerable<QuizDTO>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetQuizzesByCollege(int collegeId)
        //{
        //    try
        //    {
        //        var quizzes = await _quizService.GetByCollegeIdAsync(collegeId);
        //        if (quizzes == null || !quizzes.Any())
        //            return NotFound(new { message = $"No quizzes found for College ID {collegeId}." });

        //        return Ok(quizzes);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = "An error occurred while processing your request." });
        //    }
        //}

    }
}
