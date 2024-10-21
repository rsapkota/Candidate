using Candidate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Candidate.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Domain.Entities.Candidate> Candidates { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Candidates.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }
    }
}