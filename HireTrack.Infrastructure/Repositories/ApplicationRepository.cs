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
    public class ApplicationRepository:GenericRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(HireTrackDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Application>> GetByJobIdWithDetailsAsync(int jobId)
        {
            return await _context.Applications
                .Include(a => a.Candidate)
                .Include(a => a.Job)
                .Where(a => a.JobId == jobId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetByCandidateIdWithDetailsAsync(string candidateId)
        {
            return await _context.Applications
                .Include(a => a.Candidate)
                .Include(a => a.Job)
                .Where(a => a.CandidateId == candidateId)
                .ToListAsync();
        }

        public async Task<Application?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Applications
                .Include(a => a.Candidate)
                .Include(a => a.Job)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> HasAppliedAsync(string candidateId, int jobId)
        {
            return await _context.Applications
                .AnyAsync(a => a.CandidateId == candidateId && a.JobId == jobId);
        }
    }
}
