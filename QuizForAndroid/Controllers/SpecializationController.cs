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
    /// Controller responsible for specialization management operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecializationController"/> class.
        /// </summary>
        /// <param name="specializationService">Service for specialization-related operations.</param>
        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        /// <summary>
        /// Retrieves all specializations.
        /// </summary>
        /// <returns>A list of specializations.</returns>
        /// <response code="200">Returns the list of specializations.</response>
        [HttpGet(Name = "GetAllSpecializations")]
        [ProducesResponseType(typeof(IEnumerable<SpecializationDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSpecializations()
        {
            /// <summary>
            /// Retrieves all specializations.
            /// </summary>
            /// <returns>ActionResult with list of specializations.</returns>
            try
            {
                var specializations = await _specializationService.GetAllAsync();
                return Ok(specializations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a specialization by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the specialization.</param>
        /// <returns>The specialization details.</returns>
        /// <response code="200">Returns the specialization details.</response>
        /// <response code="404">If the specialization is not found.</response>
        [HttpGet("{id}", Name = "GetSpecializationById")]
        [ProducesResponseType(typeof(SpecializationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSpecializationById(int id)
        {
            /// <summary>
            /// Retrieves a specialization by ID.
            /// </summary>
            /// <param name="id">Specialization ID.</param>
            /// <returns>ActionResult with specialization data.</returns>
            try
            {
                var specialization = await _specializationService.GetByIdAsync(id);
                if (specialization == null)
                    return NotFound(new { message = $"Specialization with ID {id} not found." });

                return Ok(specialization);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new specialization.
        /// </summary>
        /// <param name="model">The specialization details.</param>
        /// <returns>The created specialization.</returns>
        /// <response code="201">Returns the created specialization.</response>
        /// <response code="400">If the creation fails due to invalid input or other errors.</response>
        [HttpPost(Name = "CreateSpecialization")]
        [ProducesResponseType(typeof(SpecializationDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSpecialization([FromBody] SpecializationDTO model)
        {
            /// <summary>
            /// Creates a new specialization.
            /// </summary>
            /// <param name="model">Specialization data.</param>
            /// <returns>ActionResult with created specialization data.</returns>
            try
            {
                var specialization = await _specializationService.AddAsync(model);
                return CreatedAtAction(nameof(GetSpecializationById), new { id = specialization.SpecializationID }, specialization);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing specialization.
        /// </summary>
        /// <param name="id">The unique identifier of the specialization to update.</param>
        /// <param name="model">The updated specialization details.</param>
        /// <returns>A success message upon successful update.</returns>
        /// <response code="200">If the update is successful.</response>
        /// <response code="400">If the update fails due to invalid input or other errors.</response>
        /// <response code="404">If the specialization is not found.</response>
        [HttpPut("{id}", Name = "UpdateSpecialization")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSpecialization(int id, [FromBody] SpecializationDTO model)
        {
            /// <summary>
            /// Updates a specialization.
            /// </summary>
            /// <param name="id">Specialization ID.</param>
            /// <param name="model">Updated specialization data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var specialization = await _specializationService.GetByIdAsync(id);
                if (specialization == null)
                    return NotFound(new { message = $"Specialization with ID {id} not found." });

                model.SpecializationID = id;
                await _specializationService.UpdateAsync(model);
                return Ok(new { message = "Specialization updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a specialization by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the specialization to delete.</param>
        /// <returns>A success message upon successful deletion.</returns>
        /// <response code="200">If the deletion is successful.</response>
        /// <response code="404">If the specialization is not found.</response>
        [HttpDelete("{id}", Name = "DeleteSpecialization")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            /// <summary>
            /// Deletes a specialization.
            /// </summary>
            /// <param name="id">Specialization ID.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var specialization = await _specializationService.GetByIdAsync(id);
                if (specialization == null)
                    return NotFound(new { message = $"Specialization with ID {id} not found." });

                await _specializationService.DeleteAsync(id);
                return Ok(new { message = "Specialization deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
