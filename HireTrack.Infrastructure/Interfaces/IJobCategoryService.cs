using HireTrack.CORE.DTOs.JobCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Interfaces
{
    public interface IJobCategoryService
    {
        Task<IEnumerable<JobCategoryDto>> GetAllAsync();
        Task<JobCategoryDto?> GetByIdAsync(int id);
        Task<JobCategoryDto> CreateAsync(CreateJobCategoryDto dto);
        Task<JobCategoryDto> UpdateAsync(int id, CreateJobCategoryDto dto);
        Task DeleteAsync(int id);
    }
}
