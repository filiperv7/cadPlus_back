using CadPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadPlus.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed for Profiles
            modelBuilder.Entity<Profile>().HasData(
                new Profile { Id = 1, Name = "Admin" },
                new Profile { Id = 2, Name = "Médico(a)" },
                new Profile { Id = 3, Name = "Enfermeiro(a)" },
                new Profile { Id = 4, Name = "Paciente" }
            );
        }
    }
}
