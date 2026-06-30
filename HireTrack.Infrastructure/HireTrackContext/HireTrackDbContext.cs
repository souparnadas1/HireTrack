using HireTrack.CORE.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.HireTrackContext
{
    public class HireTrackDbContext : IdentityDbContext<ApplicationUser>
    {
        public HireTrackDbContext(DbContextOptions<HireTrackDbContext> options) : base(options)
        {

        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Soft delete global filter
            builder.Entity<Job>().HasQueryFilter(j => !j.IsDeleted);
            builder.Entity<JobCategory>().HasQueryFilter(jc => !jc.IsDeleted);
            builder.Entity<Application>().HasQueryFilter(a => !a.IsDeleted);

            // Job → Employer (restrict delete so employer can't be deleted if they have jobs)
            builder.Entity<Job>()
                .HasOne(j => j.Employer)
                .WithMany(u => u.PostedJobs)
                .HasForeignKey(j => j.EmployerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Job → Category
            builder.Entity<Job>()
                .HasOne(j => j.Category)
                .WithMany(c => c.Jobs)
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Application → Candidate
            builder.Entity<Application>()
                .HasOne(a => a.Candidate)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Application → Job
            builder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            // Salary precision
            builder.Entity<Job>()
                .Property(j => j.Salary)
                .HasColumnType("decimal(18,2)");
        }

    }
}
