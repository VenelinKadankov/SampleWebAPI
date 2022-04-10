namespace TheRecrutmentTool.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants.Skill;

    public class Skill
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<CandidateSkill> CandidateSkills { get; set; } = new HashSet<CandidateSkill>();

        public ICollection<JobSkill> JobSkill { get; set; } = new HashSet<JobSkill>();
    }
}
