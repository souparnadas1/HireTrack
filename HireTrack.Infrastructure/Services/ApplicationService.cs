using HireTrack.CORE.DTOs.Application;
using HireTrack.CORE.Entities;
using HireTrack.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HireTrack.CORE.Enums;

namespace HireTrack.Infrastructure.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationDto> ApplyAsync(CreateApplicationDto dto, string candidateId)
        {
            // Check if job exists
            var job = await _unitOfWork.Jobs.GetByIdAsync(dto.JobId);
            if (job == null)
                throw new Exception("Job not found.");

            // Check if job is active
            if (!job.IsActive)
                throw new Exception("This job is no longer active.");

            // Prevent duplicate application
            var alreadyApplied = await _unitOfWork.Applications.HasAppliedAsync(candidateId, dto.JobId);
            if (alreadyApplied)
                throw new Exception("You have already applied for this job.");

            var application = new Application
            {
                CandidateId = candidateId,
                JobId = dto.JobId,
                Status = ApplicationStatus.Applied,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Applications.AddAsync(application);
            await _unitOfWork.SaveChangesAsync();

            var created = await _unitOfWork.Applications.GetByIdWithDetailsAsync(application.Id);
            return MapToDto(created!);
        }

        public async Task<IEnumerable<ApplicationDto>> GetMyApplicationsAsync(string candidateId)
        {
            var applications = await _unitOfWork.Applications.GetByCandidateIdWithDetailsAsync(candidateId);
            return applications.Select(MapToDto);
        }

        public async Task<IEnumerable<ApplicationDto>> GetApplicantsForJobAsync(int jobId, string employerId)
        {
            // Verify the job belongs to this employer
            var job = await _unitOfWork.Jobs.GetByIdAsync(jobId);
            if (job == null)
                throw new Exception("Job not found.");

            if (job.EmployerId != employerId)
                throw new Exception("You are not authorized to view applicants for this job.");

            var applications = await _unitOfWork.Applications.GetByJobIdWithDetailsAsync(jobId);
            return applications.Select(MapToDto);
        }

        public async Task<ApplicationDto> UpdateStatusAsync(int applicationId, UpdateApplicationStatusDto dto, string employerId)
        {
            var application = await _unitOfWork.Applications.GetByIdWithDetailsAsync(applicationId);
            if (application == null)
                throw new Exception("Application not found.");

            // Verify the job belongs to this employer
            if (application.Job.EmployerId != employerId)
                throw new Exception("You are not authorized to update this application.");

            application.Status = dto.Status;
            _unitOfWork.Applications.Update(application);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(application);
        }

        private static ApplicationDto MapToDto(Application application) => new()
        {
            Id = application.Id,
            CandidateName = application.Candidate?.FullName ?? string.Empty,
            CandidateEmail = application.Candidate?.Email ?? string.Empty,
            JobTitle = application.Job?.Title ?? string.Empty,
            Status = application.Status.ToString(),
            AppliedAt = application.CreatedAt
        };
    }
}
