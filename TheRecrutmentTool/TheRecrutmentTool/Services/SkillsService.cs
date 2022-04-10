namespace TheRecrutmentTool.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TheRecrutmentTool.Data;
    using TheRecrutmentTool.Services.Models;

    public class SkillsService : ISkillsService
    {
        private readonly ApplicationDbContext data;

        public SkillsService(ApplicationDbContext data)
            => this.data = data;

        public ICollection<SkillServiceModel> GetActive()
        {
            var skillCollection = new List<SkillServiceModel>();

            foreach (var candidateSkil in this.data.CandidateSkills)
            {
                if (this.data.Candidates.Select(c => c.Id).Contains(candidateSkil.CandidateId))
                {
                    skillCollection.Add(new SkillServiceModel
                    {
                        Name = this.data.Skills.FirstOrDefault(s => s.Id == candidateSkil.SkillId)?.Name,
                    });
                }
            }

            return skillCollection;
        }

        public SkillServiceModel GetById(int id)
        {
            var skill = this.data.Skills.FirstOrDefault(s => s.Id == id);

            if (skill == null)
            {
                return null;
            }

            var returnSkill = new SkillServiceModel
            {
                Name = skill.Name
            };

            return returnSkill;
        }
    }
}
