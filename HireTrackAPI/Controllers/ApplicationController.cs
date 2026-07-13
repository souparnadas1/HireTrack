using HireTrack.CORE.DTOs.Application;
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
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // Candidate applies to a job
        [HttpPost]
        [Authorize(Roles = Roles.Candidate)]
        public async Task<IActionResult> Apply(CreateApplicationDto dto)
        {
            try
            {
                var candidateId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (candidateId == null)
                    return Unauthorized();

                var result = await _applicationService.ApplyAsync(dto, candidateId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Candidate views their own applications
        [HttpGet("my-applications")]
        [Authorize(Roles = Roles.Candidate)]
        public async Task<IActionResult> GetMyApplications()
        {
            var candidateId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (candidateId == null)
                return Unauthorized();

            var result = await _applicationService.GetMyApplicationsAsync(candidateId);
            return Ok(result);
        }

        // Employer views applicants for their job
        [HttpGet("job/{jobId}/applicants")]
        [Authorize(Roles = Roles.Employer)]
        public async Task<IActionResult> GetApplicants(int jobId)
        {
            try
            {
                var employerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (employerId == null)
                    return Unauthorized();

                var result = await _applicationService.GetApplicantsForJobAsync(jobId, employerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Employer updates application status
        [HttpPut("{applicationId}/status")]
        [Authorize(Roles = Roles.Employer)]
        public async Task<IActionResult> UpdateStatus(int applicationId, UpdateApplicationStatusDto dto)
        {
            try
            {
                var employerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (employerId == null)
                    return Unauthorized();

                var result = await _applicationService.UpdateStatusAsync(applicationId, dto, employerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
