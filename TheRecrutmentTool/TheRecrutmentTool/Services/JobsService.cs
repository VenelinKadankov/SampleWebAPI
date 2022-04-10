namespace TheRecrutmentTool.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TheRecrutmentTool.Data;
    using TheRecrutmentTool.Models;
    using TheRecrutmentTool.Services.Models;

    public class JobsService : IJobsService
    {
        private readonly ApplicationDbContext data;

        public JobsService(ApplicationDbContext data)
            => this.data = data;

        public async Task CreateJob(JobServiceModel model)
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

            var job = new Job
            {
                Name = model.Title,
                Description = model.Description,
                Salary = model.Salary,
            };

            await this.data.Jobs.AddAsync(job);
            await this.data.SaveChangesAsync();

            var jobFromDatabase = this.data.Jobs.FirstOrDefault(j => j.Description == model.Description && j.Salary == model.Salary);

            foreach (var skill in skills)
            {
                if (jobFromDatabase != null)
                {
                    var contains = this.data.JobSkills.Any(js => js.JobId == jobFromDatabase.Id && js.SkillId == skill.Id);

                    if (contains == false)
                    {
                        await this.data.JobSkills.AddAsync(new JobSkill 
                        { 
                            Job = jobFromDatabase,
                            Skill = skill,
                        });
                        await this.data.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
