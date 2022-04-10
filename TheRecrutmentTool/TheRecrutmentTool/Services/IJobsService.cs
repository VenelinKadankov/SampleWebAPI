namespace TheRecrutmentTool.Services
{
    using System.Threading.Tasks;
    using TheRecrutmentTool.Services.Models;

    public interface IJobsService
    {
        Task CreateJob(JobServiceModel model);
    }
}
