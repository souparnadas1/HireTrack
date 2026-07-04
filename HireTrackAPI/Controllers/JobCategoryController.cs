using HireTrack.CORE.DTOs.JobCategory;
using HireTrack.CORE.Entities;
using HireTrack.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoryController : ControllerBase
    {
        private readonly IJobCategoryService _jobCategoryService;

        public JobCategoryController(IJobCategoryService jobCategoryService)
        {
            _jobCategoryService = jobCategoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _jobCategoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _jobCategoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound(new { message = "Category not found." });

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create(CreateJobCategoryDto dto)
        {
            try
            {
                var result = await _jobCategoryService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, CreateJobCategoryDto dto)
        {
            try
            {
                var result = await _jobCategoryService.UpdateAsync(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _jobCategoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
