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
    /// Controller responsible for college management operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollegeController : ControllerBase
    {
        private readonly ICollegeService _collegeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollegeController"/> class.
        /// </summary>
        /// <param name="collegeService">Service for college-related operations.</param>
        public CollegeController(ICollegeService collegeService)
        {
            _collegeService = collegeService;
        }

        /// <summary>
        /// Retrieves all colleges.
        /// </summary>
        /// <returns>A list of colleges.</returns>
        /// <response code="200">Returns the list of colleges.</response>
        [HttpGet(Name = "GetAllColleges")]
        [ProducesResponseType(typeof(IEnumerable<CollegeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllColleges()
        {
            /// <summary>
            /// Retrieves all colleges.
            /// </summary>
            /// <returns>ActionResult with list of colleges.</returns>
            try
            {
                var colleges = await _collegeService.GetAllAsync();
                return Ok(colleges);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a college by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the college.</param>
        /// <returns>The college details.</returns>
        /// <response code="200">Returns the college details.</response>
        /// <response code="404">If the college is not found.</response>
        [HttpGet("{id}", Name = "GetCollegeById")]
        [ProducesResponseType(typeof(CollegeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCollegeById(int id)
        {
            /// <summary>
            /// Retrieves a college by ID.
            /// </summary>
            /// <param name="id">College ID.</param>
            /// <returns>ActionResult with college data.</returns>
            try
            {
                var college = await _collegeService.GetByIdAsync(id);
                if (college == null)
                    return NotFound(new { message = $"College with ID {id} not found." });

                return Ok(college);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new college.
        /// </summary>
        /// <param name="model">The college details.</param>
        /// <returns>The created college.</returns>
        /// <response code="201">Returns the created college.</response>
        /// <response code="400">If the creation fails due to invalid input or other errors.</response>
        [HttpPost(Name = "CreateCollege")]
        [ProducesResponseType(typeof(CollegeDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> CreateCollege([FromBody] CollegeDTO model)
        {
            /// <summary>
            /// Creates a new college.
            /// </summary>
            /// <param name="model">College data.</param>
            /// <returns>ActionResult with created college data.</returns>
            try
            {
                var college = await _collegeService.AddAsync(model);
                return CreatedAtAction(nameof(GetCollegeById), new { id = college.CollegeID }, college);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing college.
        /// </summary>
        /// <param name="id">The unique identifier of the college to update.</param>
        /// <param name="model">The updated college details.</param>
        /// <returns>A success message upon successful update.</returns>
        /// <response code="200">If the update is successful.</response>
        /// <response code="400">If the update fails due to invalid input or other errors.</response>
        /// <response code="404">If the college is not found.</response>
        [HttpPut("{id}", Name = "UpdateCollege")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> UpdateCollege(int id, [FromBody] CollegeDTO model)
        {
            /// <summary>
            /// Updates a college.
            /// </summary>
            /// <param name="id">College ID.</param>
            /// <param name="model">Updated college data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var college = await _collegeService.GetByIdAsync(id);
                if (college == null)
                    return NotFound(new { message = $"College with ID {id} not found." });

                model.CollegeID = id;
                await _collegeService.UpdateAsync(model);
                return Ok(new { message = "College updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a college by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the college to delete.</param>
        /// <returns>A success message upon successful deletion.</returns>
        /// <response code="200">If the deletion is successful.</response>
        /// <response code="404">If the college is not found.</response>
        [HttpDelete("{id}", Name = "DeleteCollege")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> DeleteCollege(int id)
        {
            /// <summary>
            /// Deletes a college.
            /// </summary>
            /// <param name="id">College ID.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var college = await _collegeService.GetByIdAsync(id);
                if (college == null)
                    return NotFound(new { message = $"College with ID {id} not found." });

                await _collegeService.DeleteAsync(id);
                return Ok(new { message = "College deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
