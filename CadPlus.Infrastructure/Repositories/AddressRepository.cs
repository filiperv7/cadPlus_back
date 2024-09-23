using CadPlus.Domain.Entities;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CadPlus.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Address> CheckIfAddresAlreadyExists(string zipCode, string street)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.ZipCode == zipCode && a.Street == street);
        }

        public async Task RemoveUserAddresses(Guid userId, List<Guid> addressesExcluded)
        {
            var user = await _context.Users.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                user.Addresses.RemoveAll(a => addressesExcluded.Contains(a.Id));
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAddress(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }
    }
}
