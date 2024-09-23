using CadPlus.Domain.Entities;

namespace CadPlus.Domain.Interfaces.IServices
{
    public interface IAddressService
    {
        public Task<List<Address>> HandleWithAddresses(List<Address> addresses);
    }
}
