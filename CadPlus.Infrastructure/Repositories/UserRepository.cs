using CadPlus.Domain.Entities;
using CadPlus.Domain.Enums;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CadPlus.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Include(u => u.Profiles).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersByProfile(int idProfile)
        {
            return await _context.Users
            .Include(u => u.Profiles)
            .Include(u => u.Addresses)
            .Where(u => u.Profiles.Any(p => p.Id == idProfile) && !u.Excluded)
            .ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users
                .Include(u => u.Profiles)
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Id == id && !u.Excluded);
        }

        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                user.Excluded = true;
                user.ExclusionDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Profile> GetProfileById(int id)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> CheckIfEmailAlreadyUsed(string email)
        {
            if (await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.Excluded) == null) return false;

            return true;
        }

        public async Task<bool> CheckIfCpfAlreadyUsed(string cpf)
        {
            if (await _context.Users.FirstOrDefaultAsync(u => u.CPF == cpf && !u.Excluded) == null) return false;

            return true;
        }
    }
}
