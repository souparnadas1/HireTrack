using HireTrack.CORE.DTOs.Job;
using HireTrack.CORE.Entities;
using HireTrack.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HireTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet("getAllJobs")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllActive()
        {
            var jobs = await _jobService.GetAllActiveJobsAsync();
            return Ok(jobs);
        }

        [HttpGet("getjobBy/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _jobService.GetByIdAsync(id);
            if (job == null)
                return NotFound(new { message = "Job not found." });

            return Ok(job);
        }

        [HttpGet("my-jobs")]
        [Authorize(Roles = Roles.Employer)]
        public async Task<IActionResult> GetMyJobs()
        {
            var employerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (employerId == null)
                return Unauthorized();

            var jobs = await _jobService.GetJobsByEmployerAsync(employerId);
            return Ok(jobs);
        }

        [HttpPost("createJob")]
        [Authorize(Roles = Roles.Employer)]
        public async Task<IActionResult> Create(CreateJobDto dto)
        {
            try
            {
                var employerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (employerId == null)
                    return Unauthorized();

                var result = await _jobService.CreateAsync(dto, employerId);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateJob/{id}")]
        [Authorize(Roles = Roles.Employer)]
        public async Task<IActionResult> Update(int id, CreateJobDto dto)
        {
            try
            {
                var employerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (employerId == null)
                    return Unauthorized();

                var result = await _jobService.UpdateAsync(id, dto, employerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("deleteJob/{id}")]
        [Authorize(Roles = Roles.Employer)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (employerId == null)
                    return Unauthorized();

                await _jobService.DeleteAsync(id, employerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
