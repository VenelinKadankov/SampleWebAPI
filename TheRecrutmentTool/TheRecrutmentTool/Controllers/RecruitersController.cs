namespace TheRecrutmentTool.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using TheRecrutmentTool.Services;
    using TheRecrutmentTool.Services.Models;

    [ApiController]
    [Route("recruiters")]
    public class RecruitersController : ControllerBase
    {

        private readonly IRecruitersService recruiterssService;
        public RecruitersController(IRecruitersService recruiterssService)
            => this.recruiterssService = recruiterssService;

        [HttpGet]
        public IEnumerable<RecruiterServiceModel> Get()
        {
            var result = this.recruiterssService.All();

            if (result == null)
            {
                return new List<RecruiterServiceModel>();
            }

            return result;
        }

        //[HttpGet]
        //public IEnumerable<RecruiterServiceModel> GetAboveLevel([FromQuery] int level)
        //{
        //    var result = this.recruiterssService.AllAboveLevel(level);

        //    if (result == null)
        //    {
        //        return new List<RecruiterServiceModel>();
        //    }

        //    return result;
       // }
    }
}
