namespace TheRecrutmentTool.Data
{
    using Microsoft.EntityFrameworkCore;
    using TheRecrutmentTool.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; init; }

        public DbSet<Interview> Interviews { get; init; }

        public DbSet<Job> Jobs { get; init; }

        public DbSet<CandidateSkill> CandidateSkills { get; init; }

        public DbSet<JobSkill> JobSkills { get; init; }

        public DbSet<Recruiter> Recruiters { get; init; }

        public DbSet<Skill> Skills { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseSqlServer("Server=.;Database=TheRecrutmentTool;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Job>()
                .Property(j => j.Salary)
                .HasPrecision(10, 2);

            builder.Entity<Candidate>()
                .HasOne(c => c.Recruiter)
                .WithMany(r => r.Candidates)
                .HasForeignKey(c => c.RecruiterId);

            builder.Entity<Interview>()
                .HasKey(c => new { c.JobId, c.CandidateId });

            builder.Entity<CandidateSkill>()
                .HasKey(c => new { c.CandidateId, c.SkillId });

            builder.Entity<JobSkill>()
                .HasKey(c => new { c.JobId, c.SkillId });

            base.OnModelCreating(builder);
        }
    }
}
