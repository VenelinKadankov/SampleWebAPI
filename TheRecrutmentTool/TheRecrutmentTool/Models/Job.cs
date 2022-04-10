namespace TheRecrutmentTool.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants.Job;

    public class Job
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public decimal Salary { get; set; }

        [Required]
        public ICollection<JobSkill> JobSkills { get; set; } = new HashSet<JobSkill>();

        [Required]
        public ICollection<Interview> Interviews { get; set; } = new HashSet<Interview>();
    }
}
