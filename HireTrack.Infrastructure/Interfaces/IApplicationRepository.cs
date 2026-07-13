using HireTrack.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Interfaces
{
    public interface IApplicationRepository : IGenericRepository<Application>
    {
        Task<IEnumerable<Application>> GetByJobIdWithDetailsAsync(int jobId);
        Task<IEnumerable<Application>> GetByCandidateIdWithDetailsAsync(string candidateId);
        Task<Application?> GetByIdWithDetailsAsync(int id);
        Task<bool> HasAppliedAsync(string candidateId, int jobId);
    }
}
