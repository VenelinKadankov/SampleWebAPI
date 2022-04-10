namespace TheRecrutmentTool.Services
{
    using System.Collections.Generic;
    using TheRecrutmentTool.Services.Models;

    public interface IValidator
    {
        ICollection<string> ValidateCandidate(CandidateServiceModel model);
        ICollection<string> ValidateJob(JobServiceModel model);
    }
}
