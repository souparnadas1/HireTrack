using HireTrack.CORE.DTOs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireTrack.Infrastructure.Interfaces
{
    public interface IApplicationService
    {
        Task<ApplicationDto> ApplyAsync(CreateApplicationDto dto, string candidateId);
        Task<IEnumerable<ApplicationDto>> GetMyApplicationsAsync(string candidateId);
        Task<IEnumerable<ApplicationDto>> GetApplicantsForJobAsync(int jobId, string employerId);
        Task<ApplicationDto> UpdateStatusAsync(int applicationId, UpdateApplicationStatusDto dto, string employerId);
    }
}
