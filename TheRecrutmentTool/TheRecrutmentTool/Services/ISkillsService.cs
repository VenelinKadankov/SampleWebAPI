namespace TheRecrutmentTool.Services
{
    using System.Collections.Generic;

    using TheRecrutmentTool.Services.Models;

    public interface ISkillsService
    {
        SkillServiceModel GetById(int id);

        ICollection<SkillServiceModel> GetActive();
    }
}
