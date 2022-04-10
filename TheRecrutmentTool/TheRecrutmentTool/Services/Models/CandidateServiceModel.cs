namespace TheRecrutmentTool.Services.Models
{
	using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class CandidateServiceModel
    {
        public string FirstName { get; set; }

		public string LastName { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public string BirthDate { get; set; }

        public IEnumerable<SkillServiceModel> Skills { get; set; }

		public RecruiterServiceModel Recruiter { get; set; }
    }
}