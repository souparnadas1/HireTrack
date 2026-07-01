using HireTrack.CORE.Entities;
using HireTrack.Infrastructure.HireTrackContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(
        HireTrackDbContext context,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
        {
            // Seed Roles
            string[] roles = ["Admin", "Employer", "Candidate"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed Job Categories
            if (!await context.JobCategories.AnyAsync())
            {
                var categories = new List<JobCategory>
            {
                new() { Name = "Information Technology" },
                new() { Name = "Finance & Accounting" },
                new() { Name = "Marketing" },
                new() { Name = "Human Resources" },
                new() { Name = "Sales" },
                new() { Name = "Engineering" },
                new() { Name = "Healthcare" },
                new() { Name = "Education" },
            };

                await context.JobCategories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Admin User
            string adminEmail = "admin@hiretrack.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    FullName = "Super Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
