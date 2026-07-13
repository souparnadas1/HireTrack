using HireTrack.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<JobCategory> JobCategories { get; }
        IJobRepository Jobs { get; }
        IApplicationRepository Applications { get; }
        Task<int> SaveChangesAsync();
    }
}
