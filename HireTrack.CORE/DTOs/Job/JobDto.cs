using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.CORE.DTOs.Job
{
    public class JobDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string JobType { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsActive { get; set; }
        public string EmployerName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
