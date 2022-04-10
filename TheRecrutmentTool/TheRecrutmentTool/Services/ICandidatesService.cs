namespace TheRecrutmentTool.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TheRecrutmentTool.Services.Models;

    public interface ICandidatesService
    {
        CandidateServiceModel GetById(int id);

        Task CreateCandidate(CandidateServiceModel model);

        Task ChangeCandidate(CandidateServiceModel model, int id);

        void DeleteCandidate(int id);
    }
}
