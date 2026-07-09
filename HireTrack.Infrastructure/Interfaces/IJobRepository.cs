using HireTrack.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<IEnumerable<Job>> GetAllActiveWithDetailsAsync();
        Task<IEnumerable<Job>> GetByEmployerWithDetailsAsync(string employerId);
        Task<Job?> GetByIdWithDetailsAsync(int id);
    }
}
