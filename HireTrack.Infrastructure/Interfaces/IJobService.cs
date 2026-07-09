using HireTrack.CORE.DTOs.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<JobDto>> GetAllActiveJobsAsync();
        Task<IEnumerable<JobDto>> GetJobsByEmployerAsync(string employerId);
        Task<JobDto?> GetByIdAsync(int id);
        Task<JobDto> CreateAsync(CreateJobDto dto, string employerId);
        Task<JobDto> UpdateAsync(int id, CreateJobDto dto, string employerId);
        Task DeleteAsync(int id, string employerId);
    }
}
