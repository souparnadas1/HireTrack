using HireTrack.CORE.DTOs.JobCategory;
using HireTrack.CORE.Entities;
using HireTrack.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Services
{
    public class JobCategoryService : IJobCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<JobCategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.JobCategories.GetAllAsync();
            return categories.Select(c => new JobCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<JobCategoryDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.JobCategories.GetByIdAsync(id);
            if (category == null) return null;

            return new JobCategoryDto { Id = category.Id, Name = category.Name };
        }

        public async Task<JobCategoryDto> CreateAsync(CreateJobCategoryDto dto)
        {
            var category = new JobCategory { Name = dto.Name };
            await _unitOfWork.JobCategories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return new JobCategoryDto { Id = category.Id, Name = category.Name };
        }

        public async Task<JobCategoryDto> UpdateAsync(int id, CreateJobCategoryDto dto)
        {
            var category = await _unitOfWork.JobCategories.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Category not found.");

            category.Name = dto.Name;
            _unitOfWork.JobCategories.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return new JobCategoryDto { Id = category.Id, Name = category.Name };
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.JobCategories.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Category not found.");

            _unitOfWork.JobCategories.Delete(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
