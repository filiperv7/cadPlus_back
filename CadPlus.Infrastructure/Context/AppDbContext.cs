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

            // Seed for Address
            var addressId = Guid.NewGuid();
            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = addressId,
                    ZipCode = "72546830",
                    Street = "Rua",
                    City = "Cidade",
                    State = "Estado"
                }
            );

            // Seed for User (Admin)
            var adminUserId = Guid.NewGuid();
            var creationDate = DateTime.UtcNow;
            modelBuilder.Entity<User>().HasData(
                new User(
                    "61426796021",
                    "Admin Default",
                    "admin@default.com",
                    "$2a$11$1tCSX4GPbhHrOgzl5ZsP3u.oFfiUW/Nlu03XtTEifyZ1tpX8.TaUC", // => Adm!n123
                    "61998574820",
                    0
                )
                {
                    Id = adminUserId,
                    CreationDate = creationDate,
                    Excluded = false
                }
            );

            // Establishing Many-to-Many relationship for User and Profiles
            modelBuilder.Entity<User>()
                .HasMany(u => u.Profiles)
                .WithMany(p => p.Users)
                .UsingEntity(j => j.HasData(
                    new { UsersId = adminUserId, ProfilesId = 1 }
                ));

            // Establishing Many-to-Many relationship for User and Addresses
            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithMany(a => a.Users)
                .UsingEntity(j => j.HasData(
                    new { UsersId = adminUserId, AddressesId = addressId }
                ));
        }
    }
}
