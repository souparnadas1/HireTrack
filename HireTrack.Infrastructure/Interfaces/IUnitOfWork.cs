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
        IGenericRepository<Job> Jobs { get; }
        IGenericRepository<Application> Applications { get; }
        Task<int> SaveChangesAsync();
    }
}
