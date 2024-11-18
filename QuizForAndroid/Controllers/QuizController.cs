using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.DAL.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace QuizForAndroid.API.Controllers
{
    /// <summary>
    /// Controller responsible for quiz management operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
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
        [Authorize(Roles = "Admin, Editor")]
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
        [Authorize(Roles = "Admin, Editor")]
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
        [Authorize(Roles = "Admin, Editor")]
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

        /// <summary>
        /// Adds a full quiz with its questions and choices at once.
        /// </summary>
        /// <param name="model">The full quiz details, including questions and choices.</param>
        /// <returns>The created full quiz.</returns>
        /// <response code="201">Returns the created full quiz.</response>
        /// <response code="400">If the creation fails due to invalid input or other errors.</response>
        [HttpPost("full-quiz", Name = "AddFullQuiz")]
        [ProducesResponseType(typeof(FullQuizDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> AddFullQuiz([FromBody] FullQuizDTO model)
        {
            /// <summary>
            /// Adds a full quiz with questions and choices.
            /// </summary>
            /// <param name="model">Full quiz data.</param>
            /// <returns>ActionResult with created full quiz data.</returns>
            try
            {
                var quiz = await _quizService.AddFullQuizAsync(model); // edit func name
                return CreatedAtAction(nameof(GetQuizById), new { id = quiz.QuizID }, quiz);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
