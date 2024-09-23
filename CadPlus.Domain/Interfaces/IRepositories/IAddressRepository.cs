using CadPlus.Domain.Entities;

namespace CadPlus.Domain.Interfaces.IRepositories
{
    public interface IAddressRepository
    {
        Task<Address> CheckIfAddresAlreadyExists(string zipCode, string street);
        Task RemoveUserAddresses(Guid userId, List<Guid> addressesExcluded);

        Task AddAddress(Address address);
    }
}
