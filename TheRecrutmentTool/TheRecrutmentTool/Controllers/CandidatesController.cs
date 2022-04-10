namespace TheRecrutmentTool.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TheRecrutmentTool.Services;
    using TheRecrutmentTool.Services.Models;


    [ApiController]
    [Route("candidates")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidatesService candidatesService;
        private readonly IValidator validator;
        public CandidatesController(
            ICandidatesService candidatesService,
            IValidator validator)
        {
            this.candidatesService = candidatesService;
            this.validator = validator;
        }

        [HttpPost]
        public async Task Post([FromBody] CandidateServiceModel model)
        {
            var errors = this.validator.ValidateCandidate(model);

            if (errors.Count > 0)
            {
                return;
            }

            await this.candidatesService.CreateCandidate(model);
        }

        [HttpGet]
        [Route("{id}")]
        public CandidateServiceModel Get(int id)
        {
            var result = this.candidatesService.GetById(id);

            return result;
        }


        [HttpPut]
        [Route("{id}")]
        public async Task Put(int id, [FromBody] CandidateServiceModel model)
        {
            var errors = this.validator.ValidateCandidate(model);

            if (errors.Count > 0)
            {
                return;
            }

            await this.candidatesService.ChangeCandidate(model, id);
        }


        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            this.candidatesService.DeleteCandidate(id);
        }
    }
}
