using HireTrack.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.CORE.Entities
{
    public class Job : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public JobType JobType { get; set; }
        public decimal? Salary { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsActive { get; set; } = true;

        public string EmployerId { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public ApplicationUser Employer { get; set; } = null!;
        public JobCategory Category { get; set; } = null!;
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
