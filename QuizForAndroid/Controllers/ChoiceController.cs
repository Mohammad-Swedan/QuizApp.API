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
    /// Controller responsible for choice management operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChoiceController : ControllerBase
    {
        private readonly IChoiceService _choiceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChoiceController"/> class.
        /// </summary>
        /// <param name="choiceService">Service for choice-related operations.</param>
        public ChoiceController(IChoiceService choiceService)
        {
            _choiceService = choiceService;
        }

        /// <summary>
        /// Retrieves all choices.
        /// </summary>
        /// <returns>A list of choices.</returns>
        /// <response code="200">Returns the list of choices.</response>
        [HttpGet(Name = "GetAllChoices")]
        [ProducesResponseType(typeof(IEnumerable<ChoiceDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllChoices()
        {
            /// <summary>
            /// Retrieves all choices.
            /// </summary>
            /// <returns>ActionResult with list of choices.</returns>
            try
            {
                var choices = await _choiceService.GetAllAsync();
                return Ok(choices);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a choice by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the choice.</param>
        /// <returns>The choice details.</returns>
        /// <response code="200">Returns the choice details.</response>
        /// <response code="404">If the choice is not found.</response>
        [HttpGet("{id}", Name = "GetChoiceById")]
        [ProducesResponseType(typeof(ChoiceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChoiceById(int id)
        {
            /// <summary>
            /// Retrieves a choice by ID.
            /// </summary>
            /// <param name="id">Choice ID.</param>
            /// <returns>ActionResult with choice data.</returns>
            try
            {
                var choice = await _choiceService.GetByIdAsync(id);
                if (choice == null)
                    return NotFound(new { message = $"Choice with ID {id} not found." });

                return Ok(choice);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new choice.
        /// </summary>
        /// <param name="model">The choice details.</param>
        /// <returns>The created choice.</returns>
        /// <response code="201">Returns the created choice.</response>
        /// <response code="400">If the creation fails due to invalid input or other errors.</response>
        [HttpPost(Name = "CreateChoice")]
        [ProducesResponseType(typeof(ChoiceDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> CreateChoice([FromBody] ChoiceDTO model)
        {
            /// <summary>
            /// Creates a new choice.
            /// </summary>
            /// <param name="model">Choice data.</param>
            /// <returns>ActionResult with created choice data.</returns>
            try
            {
                var choice = await _choiceService.AddAsync(model);
                return CreatedAtAction(nameof(GetChoiceById), new { id = choice.ChoiceID }, choice);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing choice.
        /// </summary>
        /// <param name="id">The unique identifier of the choice to update.</param>
        /// <param name="model">The updated choice details.</param>
        /// <returns>A success message upon successful update.</returns>
        /// <response code="200">If the update is successful.</response>
        /// <response code="400">If the update fails due to invalid input or other errors.</response>
        /// <response code="404">If the choice is not found.</response>
        [HttpPut("{id}", Name = "UpdateChoice")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> UpdateChoice(int id, [FromBody] ChoiceDTO model)
        {
            /// <summary>
            /// Updates a choice.
            /// </summary>
            /// <param name="id">Choice ID.</param>
            /// <param name="model">Updated choice data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var choice = await _choiceService.GetByIdAsync(id);
                if (choice == null)
                    return NotFound(new { message = $"Choice with ID {id} not found." });

                model.ChoiceID = id;
                await _choiceService.UpdateAsync(model);
                return Ok(new { message = "Choice updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a choice by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the choice to delete.</param>
        /// <returns>A success message upon successful deletion.</returns>
        /// <response code="200">If the deletion is successful.</response>
        /// <response code="404">If the choice is not found.</response>
        [HttpDelete("{id}", Name = "DeleteChoice")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> DeleteChoice(int id)
        {
            /// <summary>
            /// Deletes a choice.
            /// </summary>
            /// <param name="id">Choice ID.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var choice = await _choiceService.GetByIdAsync(id);
                if (choice == null)
                    return NotFound(new { message = $"Choice with ID {id} not found." });

                await _choiceService.DeleteAsync(id);
                return Ok(new { message = "Choice deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
