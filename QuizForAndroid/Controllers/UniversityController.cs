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
    /// Controller responsible for university management operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _universityService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversityController"/> class.
        /// </summary>
        /// <param name="universityService">Service for university-related operations.</param>
        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        /// <summary>
        /// Retrieves all universities.
        /// </summary>
        /// <returns>A list of universities.</returns>
        /// <response code="200">Returns the list of universities.</response>
        [HttpGet(Name = "GetAllUniversities")]
        [ProducesResponseType(typeof(IEnumerable<UniversityDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUniversities()
        {
            /// <summary>
            /// Retrieves all universities.
            /// </summary>
            /// <returns>ActionResult with list of universities.</returns>
            try
            {
                var universities = await _universityService.GetAllAsync();
                return Ok(universities);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a university by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the university.</param>
        /// <returns>The university details.</returns>
        /// <response code="200">Returns the university details.</response>
        /// <response code="404">If the university is not found.</response>
        [HttpGet("{id}", Name = "GetUniversityById")]
        [ProducesResponseType(typeof(UniversityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUniversityById(int id)
        {
            /// <summary>
            /// Retrieves a university by ID.
            /// </summary>
            /// <param name="id">University ID.</param>
            /// <returns>ActionResult with university data.</returns>
            try
            {
                var university = await _universityService.GetByIdAsync(id);
                if (university == null)
                    return NotFound(new { message = $"University with ID {id} not found." });

                return Ok(university);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new university.
        /// </summary>
        /// <param name="model">The university details.</param>
        /// <returns>The created university.</returns>
        /// <response code="201">Returns the created university.</response>
        /// <response code="400">If the creation fails due to invalid input or other errors.</response>
        [HttpPost(Name = "CreateUniversity")]
        [ProducesResponseType(typeof(UniversityDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUniversity([FromBody] UniversityDTO model)
        {
            /// <summary>
            /// Creates a new university.
            /// </summary>
            /// <param name="model">University data.</param>
            /// <returns>ActionResult with created university data.</returns>
            try
            {
                var university = await _universityService.AddAsync(model);
                return CreatedAtAction(nameof(GetUniversityById), new { id = university.UniversityID }, university);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing university.
        /// </summary>
        /// <param name="id">The unique identifier of the university to update.</param>
        /// <param name="model">The updated university details.</param>
        /// <returns>A success message upon successful update.</returns>
        /// <response code="200">If the update is successful.</response>
        /// <response code="400">If the update fails due to invalid input or other errors.</response>
        /// <response code="404">If the university is not found.</response>
        [HttpPut("{id}", Name = "UpdateUniversity")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUniversity(int id, [FromBody] UniversityDTO model)
        {
            /// <summary>
            /// Updates a university.
            /// </summary>
            /// <param name="id">University ID.</param>
            /// <param name="model">Updated university data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var university = await _universityService.GetByIdAsync(id);
                if (university == null)
                    return NotFound(new { message = $"University with ID {id} not found." });

                model.UniversityID = id;
                await _universityService.UpdateAsync(model);
                return Ok(new { message = "University updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a university by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the university to delete.</param>
        /// <returns>A success message upon successful deletion.</returns>
        /// <response code="200">If the deletion is successful.</response>
        /// <response code="404">If the university is not found.</response>
        [HttpDelete("{id}", Name = "DeleteUniversity")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            /// <summary>
            /// Deletes a university.
            /// </summary>
            /// <param name="id">University ID.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var university = await _universityService.GetByIdAsync(id);
                if (university == null)
                    return NotFound(new { message = $"University with ID {id} not found." });

                await _universityService.DeleteAsync(id);
                return Ok(new { message = "University deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
