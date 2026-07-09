using HireTrack.CORE.DTOs.Job;
using HireTrack.CORE.Entities;
using HireTrack.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<JobDto>> GetAllActiveJobsAsync()
        {
            var jobs = await _unitOfWork.Jobs.GetAllActiveWithDetailsAsync();
            return jobs.Select(MapToDto);
        }

        public async Task<IEnumerable<JobDto>> GetJobsByEmployerAsync(string employerId)
        {
            var jobs = await _unitOfWork.Jobs.GetByEmployerWithDetailsAsync(employerId);
            return jobs.Select(MapToDto);
        }

        public async Task<JobDto?> GetByIdAsync(int id)
        {
            var job = await _unitOfWork.Jobs.GetByIdWithDetailsAsync(id);
            return job == null ? null : MapToDto(job);
        }

        public async Task<JobDto> CreateAsync(CreateJobDto dto, string employerId)
        {
            var job = new Job
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                JobType = dto.JobType,
                Salary = dto.Salary,
                Deadline = dto.Deadline,
                CategoryId = dto.CategoryId,
                EmployerId = employerId,
                IsActive = true
            };

            await _unitOfWork.Jobs.AddAsync(job);
            await _unitOfWork.SaveChangesAsync();

            var created = await _unitOfWork.Jobs.GetByIdWithDetailsAsync(job.Id);
            return MapToDto(created!);
        }

        public async Task<JobDto> UpdateAsync(int id, CreateJobDto dto, string employerId)
        {
            var job = await _unitOfWork.Jobs.GetByIdAsync(id);
            if (job == null)
                throw new Exception("Job not found.");

            if (job.EmployerId != employerId)
                throw new Exception("You are not authorized to update this job.");

            job.Title = dto.Title;
            job.Description = dto.Description;
            job.Location = dto.Location;
            job.JobType = dto.JobType;
            job.Salary = dto.Salary;
            job.Deadline = dto.Deadline;
            job.CategoryId = dto.CategoryId;

            _unitOfWork.Jobs.Update(job);
            await _unitOfWork.SaveChangesAsync();

            var updated = await _unitOfWork.Jobs.GetByIdWithDetailsAsync(job.Id);
            return MapToDto(updated!);
        }

        public async Task DeleteAsync(int id, string employerId)
        {
            var job = await _unitOfWork.Jobs.GetByIdAsync(id);
            if (job == null)
                throw new Exception("Job not found.");

            if (job.EmployerId != employerId)
                throw new Exception("You are not authorized to delete this job.");

            job.IsDeleted = true;
            _unitOfWork.Jobs.Update(job);
            await _unitOfWork.SaveChangesAsync();
        }

        private static JobDto MapToDto(Job job) => new()
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            Location = job.Location,
            JobType = job.JobType.ToString(),
            Salary = job.Salary,
            Deadline = job.Deadline,
            IsActive = job.IsActive,
            EmployerName = job.Employer?.FullName ?? string.Empty,
            CategoryName = job.Category?.Name ?? string.Empty
        };
    }
}
