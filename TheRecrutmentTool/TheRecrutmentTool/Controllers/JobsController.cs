namespace TheRecrutmentTool.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TheRecrutmentTool.Services;
    using TheRecrutmentTool.Services.Models;

    [ApiController]
    [Route("jobs")]

    public class JobsController : ControllerBase
    {
        private readonly IJobsService jobsService;
        private readonly IValidator validator;

        public JobsController(IJobsService jobsService, IValidator validator)
        {
            this.jobsService = jobsService;
            this.validator = validator;
        }

        [HttpPost]
        public async Task Create([FromBody] JobServiceModel model)
        {
            var errors = this.validator.ValidateJob(model);

            if (errors.Count > 0)
            {
                return;
            }

            await this.jobsService.CreateJob(model);
        }
    }
}
