namespace TheRecrutmentTool.Services
{
    using System.Collections.Generic;

    using TheRecrutmentTool.Services.Models;

    public interface IRecruitersService
    {
        IEnumerable<RecruiterServiceModel> All();

        IEnumerable<RecruiterServiceModel> AllAboveLevel(int level);
    }
}
