namespace TheRecrutmentTool.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using TheRecrutmentTool.Services;
    using TheRecrutmentTool.Services.Models;

    [ApiController]
    [Route("skills")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService skillsService;
        public SkillsController(ISkillsService skillsService)
            => this.skillsService = skillsService;

        [HttpGet]
        [Route("{id}")]
        public SkillServiceModel Get(int id)
        {
            var result = this.skillsService.GetById(id);

            if (result == null)
            {
                result = new SkillServiceModel
                {
                    Name = "Wrong ID"
                };
            }

            return result;
        }

        [HttpGet]
        [Route("active")]
        public ICollection<SkillServiceModel> Get()
        {
            var result = this.skillsService.GetActive();

            if (result == null)
            {
                result.Add(new SkillServiceModel
                {
                    Name = "No active skills available"
                });
            }

            return result;
        }
    }
}
