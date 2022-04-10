namespace TheRecrutmentTool.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants.Candidate;

    public class Candidate
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public string BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Bio { get; set; }

        [Required]
        public int RecruiterId { get; set; }

        public Recruiter Recruiter { get; set; }

        public ICollection<CandidateSkill> CandidateSkills { get; set; } = new HashSet<CandidateSkill>();

        public ICollection<Interview> Interviews { get; set; } = new HashSet<Interview>();
    }
}
