using HireTrack.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.CORE.Entities
{
    public class Application : BaseEntity
    {
        public string CandidateId { get; set; } = string.Empty;
        public int JobId { get; set; }
        public string? ResumeUrl { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;

        public ApplicationUser Candidate { get; set; } = null!;
        public Job Job { get; set; } = null!;
    }
}
