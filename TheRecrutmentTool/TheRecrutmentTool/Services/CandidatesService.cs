namespace TheRecrutmentTool.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TheRecrutmentTool.Data;
    using TheRecrutmentTool.Models;
    using TheRecrutmentTool.Services.Models;

    public class CandidatesService : ICandidatesService
    {
        private readonly ApplicationDbContext data;

        public CandidatesService(ApplicationDbContext data)
            => this.data = data;

        public async Task ChangeCandidate(CandidateServiceModel model, int id)
        {
            var skills = new HashSet<Skill>();

            foreach (var skill in model.Skills)
            {
                var newSkill = new Skill
                {
                    Name = skill.Name,
                };

                skills.Add(newSkill);

                if (!this.data.Skills.Any(s => s.Name == skill.Name))
                {
                    if (!this.data.Skills.Any(s => s.Name == newSkill.Name))
                    {
                        await this.data.Skills.AddAsync(newSkill);
                        await this.data.SaveChangesAsync();
                    }
                }
            }


            var candidate = this.data.Candidates.FirstOrDefault(c => c.Id == id);

            if (candidate == null)
            {
                await this.CreateCandidate(model);
            }

            var recruiter = this.data.Recruiters.FirstOrDefault(r => r.LastName == model.Recruiter.LastName);

            if (recruiter == null)
            {
                await this.data.Recruiters.AddAsync(new Recruiter
                {
                    LastName = model.Recruiter.LastName,
                    Email = model.Recruiter.Email,
                    Country = model.Recruiter.Country,
                });

                await this.data.SaveChangesAsync();
            }
            else if (recruiter.Candidates.Count >= 5)
            {
                return;
            }

            candidate.FirstName = model.FirstName;
            candidate.LastName = model.LastName;
            candidate.Bio = model.Bio;
            candidate.BirthDate = model.BirthDate;
            candidate.RecruiterId = recruiter.Id;

            foreach (var skill in skills)
            {
                var skillFromDatabase = this.data.Skills.FirstOrDefault(s => s.Name == skill.Name);

                if (candidate != null &&
                    skill != null &&
                    !this.data.CandidateSkills.Any(c => c.Candidate.FirstName == candidate.FirstName && c.Skill.Id == skillFromDatabase.Id))
                {
                    await this.data.CandidateSkills.AddAsync(new CandidateSkill
                    {
                        Candidate = candidate,
                        Skill = skillFromDatabase,
                    });
                }
            }

            if (!recruiter.Candidates.Any(c => c.FirstName == model.FirstName && c.LastName == model.LastName))
            {
                recruiter.Candidates.Add(candidate);

                recruiter.ExperienceLevel++;
            }

            await this.data.SaveChangesAsync();
        }

        public async Task CreateCandidate(CandidateServiceModel model)
        {
            var skills = new HashSet<Skill>();

            foreach (var skill in model.Skills)
            {
                var newSkill = new Skill
                {
                    Name = skill.Name
                };

                skills.Add(newSkill);

                if (!this.data.Skills.Select(s => s.Name).Any(n => n == newSkill.Name))
                {
                    await this.data.Skills.AddAsync(newSkill);
                    await this.data.SaveChangesAsync();
                }
            }


            var recruiter = this.data.Recruiters.FirstOrDefault(r => r.LastName == model.Recruiter.LastName && r.Email == model.Recruiter.Email);

            if (recruiter == null)
            {
                recruiter = new Recruiter
                {
                    LastName = model.Recruiter.LastName,
                    Email = model.Recruiter.Email,
                    Country = model.Recruiter.Country,
                    ExperienceLevel = 1,
                };

                await this.data.Recruiters.AddAsync(recruiter);

                await this.data.SaveChangesAsync();
            }
            else if (recruiter.Candidates.Count >= 5)
            {
                return;
            }


            recruiter.ExperienceLevel++;

            var candidate = new Candidate
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Bio = model.Bio,
                BirthDate = model.BirthDate,
                //CandidateSkills = new HashSet<CandidateSkill>{} skills,
                Recruiter = recruiter
            };

            recruiter.Candidates.Add(candidate);

            await this.data.Candidates.AddAsync(candidate);
            await this.data.SaveChangesAsync();

            //TODO: extract as method
            var candidateFromDatabase = this.data.Candidates.FirstOrDefault(c => c.FirstName == model.FirstName && c.Email == model.Email);

            foreach (var skill in skills)
            {
                var skillFromDatabase = this.data.Skills.FirstOrDefault(s => s.Name == skill.Name);

                if (candidateFromDatabase != null && skill != null)
                {
                    await this.data.CandidateSkills.AddAsync(new CandidateSkill
                    {
                        Candidate = candidateFromDatabase,
                        Skill = skillFromDatabase,
                    });
                }
            }
        }

        public void DeleteCandidate(int id)
        {
            var candidate = this.data.Candidates.FirstOrDefault(c => c.Id == id);

            if (candidate != null)
            {
                this.data.Candidates.Remove(candidate);
                this.data.SaveChanges();
            }
        }

        public CandidateServiceModel GetById(int id)
        {
            var candidate = this.data.Candidates.FirstOrDefault(c => c.Id == id);

            if (candidate == null)
            {
                return null;
            }

            var recruiter = this.data.Recruiters.FirstOrDefault(r => r.Id == candidate.RecruiterId);

            if (candidate == null)
            {
                return null;
            }

            var skills = this.data.Skills
                .Where(s => s.CandidateSkills.Any(c => c.Candidate.Id == id))
                .Select(s => new SkillServiceModel
                {
                Name = s.Name
            });

            return new CandidateServiceModel
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                Bio = candidate.Bio,
                BirthDate = candidate.BirthDate,
                Skills = skills,
                Recruiter = new RecruiterServiceModel
                {
                    LastName = recruiter.LastName,
                    Email = recruiter.Email,
                    Country = recruiter.Country
                }
            };
        }
    }
}
