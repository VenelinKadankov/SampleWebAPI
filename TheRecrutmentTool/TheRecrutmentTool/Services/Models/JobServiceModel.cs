namespace TheRecrutmentTool.Services.Models
{
    using System.Collections.Generic;

    public class JobServiceModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Salary { get; set; }

        public IEnumerable<SkillServiceModel> Skills { get; set; }
    }
}
