namespace TheRecrutmentTool.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;

    public class Recruiter
    {
        public int Id { get; init; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Country { get; set; }

        [MaxLength(MaxCandidatesCollectionLength)]
        public ICollection<Candidate> Candidates { get; set; } = new HashSet<Candidate>();

        [Range(1, int.MaxValue)]
        public int ExperienceLevel { get; set; } = 1;
    }
}
