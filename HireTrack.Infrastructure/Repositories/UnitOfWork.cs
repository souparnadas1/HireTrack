using HireTrack.CORE.Entities;
using HireTrack.Infrastructure.HireTrackContext;
using HireTrack.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HireTrackDbContext _context;

        public IGenericRepository<JobCategory> JobCategories { get; private set; }
        public IJobRepository Jobs { get; private set; }
        public IGenericRepository<Application> Applications { get; private set; }

        public UnitOfWork(HireTrackDbContext context)
        {
            _context = context;
            JobCategories = new GenericRepository<JobCategory>(context);
            Jobs = new JobRepository(context);
            Applications = new GenericRepository<Application>(context);
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }
}
