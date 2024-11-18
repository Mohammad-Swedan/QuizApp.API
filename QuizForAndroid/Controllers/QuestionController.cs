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
    /// Controller responsible for question management operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionController"/> class.
        /// </summary>
        /// <param name="questionService">Service for question-related operations.</param>
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        /// <summary>
        /// Retrieves all questions.
        /// </summary>
        /// <returns>A list of questions.</returns>
        /// <response code="200">Returns the list of questions.</response>
        [HttpGet(Name = "GetAllQuestions")]
        [ProducesResponseType(typeof(IEnumerable<QuestionDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllQuestions()
        {
            /// <summary>
            /// Retrieves all questions.
            /// </summary>
            /// <returns>ActionResult with list of questions.</returns>
            try
            {
                var questions = await _questionService.GetAllAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a question by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the question.</param>
        /// <returns>The question details.</returns>
        /// <response code="200">Returns the question details.</response>
        /// <response code="404">If the question is not found.</response>
        [HttpGet("{id}", Name = "GetQuestionById")]
        [ProducesResponseType(typeof(QuestionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            /// <summary>
            /// Retrieves a question by ID.
            /// </summary>
            /// <param name="id">Question ID.</param>
            /// <returns>ActionResult with question data.</returns>
            try
            {
                var question = await _questionService.GetByIdAsync(id);
                if (question == null)
                    return NotFound(new { message = $"Question with ID {id} not found." });

                return Ok(question);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new question.
        /// </summary>
        /// <param name="model">The question details.</param>
        /// <returns>The created question.</returns>
        /// <response code="201">Returns the created question.</response>
        /// <response code="400">If the creation fails due to invalid input or other errors.</response>
        [HttpPost(Name = "CreateQuestion")]
        [ProducesResponseType(typeof(QuestionDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDTO model)
        {
            /// <summary>
            /// Creates a new question.
            /// </summary>
            /// <param name="model">Question data.</param>
            /// <returns>ActionResult with created question data.</returns>
            try
            {
                var question = await _questionService.AddAsync(model);
                return CreatedAtAction(nameof(GetQuestionById), new { id = question.QuestionID }, question);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing question.
        /// </summary>
        /// <param name="id">The unique identifier of the question to update.</param>
        /// <param name="model">The updated question details.</param>
        /// <returns>A success message upon successful update.</returns>
        /// <response code="200">If the update is successful.</response>
        /// <response code="400">If the update fails due to invalid input or other errors.</response>
        /// <response code="404">If the question is not found.</response>
        [HttpPut("{id}", Name = "UpdateQuestion")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionDTO model)
        {
            /// <summary>
            /// Updates a question.
            /// </summary>
            /// <param name="id">Question ID.</param>
            /// <param name="model">Updated question data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var question = await _questionService.GetByIdAsync(id);
                if (question == null)
                    return NotFound(new { message = $"Question with ID {id} not found." });

                model.QuestionID = id;
                await _questionService.UpdateAsync(model);
                return Ok(new { message = "Question updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a question by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the question to delete.</param>
        /// <returns>A success message upon successful deletion.</returns>
        /// <response code="200">If the deletion is successful.</response>
        /// <response code="404">If the question is not found.</response>
        [HttpDelete("{id}", Name = "DeleteQuestion")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            /// <summary>
            /// Deletes a question.
            /// </summary>
            /// <param name="id">Question ID.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var question = await _questionService.GetByIdAsync(id);
                if (question == null)
                    return NotFound(new { message = $"Question with ID {id} not found." });

                await _questionService.DeleteAsync(id);
                return Ok(new { message = "Question deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
