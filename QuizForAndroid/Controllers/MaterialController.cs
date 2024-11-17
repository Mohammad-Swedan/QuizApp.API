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
    /// Controller responsible for material management operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialController"/> class.
        /// </summary>
        /// <param name="materialService">Service for material-related operations.</param>
        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        /// <summary>
        /// Retrieves all materials.
        /// </summary>
        /// <returns>A list of materials.</returns>
        /// <response code="200">Returns the list of materials.</response>
        [HttpGet(Name = "GetAllMaterials")]
        [ProducesResponseType(typeof(IEnumerable<MaterialDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMaterials()
        {
            /// <summary>
            /// Retrieves all materials.
            /// </summary>
            /// <returns>ActionResult with list of materials.</returns>
            try
            {
                var materials = await _materialService.GetAllAsync();
                return Ok(materials);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a material by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the material.</param>
        /// <returns>The material details.</returns>
        /// <response code="200">Returns the material details.</response>
        /// <response code="404">If the material is not found.</response>
        [HttpGet("{id}", Name = "GetMaterialById")]
        [ProducesResponseType(typeof(MaterialDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMaterialById(int id)
        {
            /// <summary>
            /// Retrieves a material by ID.
            /// </summary>
            /// <param name="id">Material ID.</param>
            /// <returns>ActionResult with material data.</returns>
            try
            {
                var material = await _materialService.GetByIdAsync(id);
                if (material == null)
                    return NotFound(new { message = $"Material with ID {id} not found." });

                return Ok(material);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new material.
        /// </summary>
        /// <param name="model">The material details.</param>
        /// <returns>The created material.</returns>
        /// <response code="201">Returns the created material.</response>
        /// <response code="400">If the creation fails due to invalid input or other errors.</response>
        [HttpPost(Name = "CreateMaterial")]
        [ProducesResponseType(typeof(MaterialDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> CreateMaterial([FromBody] MaterialDTO model)
        {
            /// <summary>
            /// Creates a new material.
            /// </summary>
            /// <param name="model">Material data.</param>
            /// <returns>ActionResult with created material data.</returns>
            try
            {
                var material = await _materialService.AddAsync(model);
                return CreatedAtAction(nameof(GetMaterialById), new { id = material.MaterialID }, material);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing material.
        /// </summary>
        /// <param name="id">The unique identifier of the material to update.</param>
        /// <param name="model">The updated material details.</param>
        /// <returns>A success message upon successful update.</returns>
        /// <response code="200">If the update is successful.</response>
        /// <response code="400">If the update fails due to invalid input or other errors.</response>
        /// <response code="404">If the material is not found.</response>
        [HttpPut("{id}", Name = "UpdateMaterial")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> UpdateMaterial(int id, [FromBody] MaterialDTO model)
        {
            /// <summary>
            /// Updates a material.
            /// </summary>
            /// <param name="id">Material ID.</param>
            /// <param name="model">Updated material data.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var material = await _materialService.GetByIdAsync(id);
                if (material == null)
                    return NotFound(new { message = $"Material with ID {id} not found." });

                model.MaterialID = id;
                await _materialService.UpdateAsync(model);
                return Ok(new { message = "Material updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a material by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the material to delete.</param>
        /// <returns>A success message upon successful deletion.</returns>
        /// <response code="200">If the deletion is successful.</response>
        /// <response code="404">If the material is not found.</response>
        [HttpDelete("{id}", Name = "DeleteMaterial")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            /// <summary>
            /// Deletes a material.
            /// </summary>
            /// <param name="id">Material ID.</param>
            /// <returns>ActionResult with status code and message.</returns>
            try
            {
                var material = await _materialService.GetByIdAsync(id);
                if (material == null)
                    return NotFound(new { message = $"Material with ID {id} not found." });

                await _materialService.DeleteAsync(id);
                return Ok(new { message = "Material deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
