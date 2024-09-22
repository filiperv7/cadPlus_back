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
    }
}
