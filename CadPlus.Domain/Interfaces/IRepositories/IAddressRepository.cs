using CadPlus.Domain.Entities;

namespace CadPlus.Domain.Interfaces.IRepositories
{
    public interface IAddressRepository
    {
        Task<Address> CheckIfAddresAlreadyExists(string zipCode, string street);
    }
}
