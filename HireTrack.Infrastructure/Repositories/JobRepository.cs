using HireTrack.CORE.Entities;
using HireTrack.Infrastructure.HireTrackContext;
using HireTrack.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Repositories
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(HireTrackDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Job>> GetAllActiveWithDetailsAsync()
        {
            return await _context.Jobs
                .Include(j => j.Employer)
                .Include(j => j.Category)
                .Where(j => j.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetByEmployerWithDetailsAsync(string employerId)
        {
            return await _context.Jobs
                .Include(j => j.Employer)
                .Include(j => j.Category)
                .Where(j => j.EmployerId == employerId)
                .ToListAsync();
        }

        public async Task<Job?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Jobs
                .Include(j => j.Employer)
                .Include(j => j.Category)
                .FirstOrDefaultAsync(j => j.Id == id);
        }
    }
}
