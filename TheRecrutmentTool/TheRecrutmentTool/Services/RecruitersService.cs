namespace TheRecrutmentTool.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using TheRecrutmentTool.Data;
    using TheRecrutmentTool.Services.Models;

    public class RecruitersService : IRecruitersService
    {
        private readonly ApplicationDbContext data;

        public RecruitersService(ApplicationDbContext data)
            => this.data = data;

        public IEnumerable<RecruiterServiceModel> All()
        {
            var recruiters = new List<RecruiterServiceModel>();

            var candidates = this.data.Candidates;

            foreach (var recruiter in this.data.Recruiters)
            {
                foreach (var candidate in candidates)
                {
                    if (candidate.RecruiterId == recruiter.Id && !recruiters.Any(r => r.LastName == recruiter.LastName))
                    {
                        recruiters.Add(new RecruiterServiceModel
                        {
                            LastName = recruiter.LastName,
                            Email = recruiter.Email,
                            Country = recruiter.Country
                        });
                    }
                }

            }

            return recruiters;
        }

        public IEnumerable<RecruiterServiceModel> AllAboveLevel(int level)
        {

            var recruiters = new List<RecruiterServiceModel>();

            foreach (var recruiter in this.data.Recruiters)
            {
                if (recruiter.ExperienceLevel == level)
                {
                    recruiters.Add(new RecruiterServiceModel
                    {
                        LastName = recruiter.LastName,
                        Email = recruiter.Email,
                        Country = recruiter.Country
                    });
                }
            }

            return recruiters;
        }
    }
}
