using CadPlus.Domain.Entities;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Domain.Interfaces.IServices;

namespace CadPlus.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<Address>> HandleWithAddresses(List<Address> addresses, bool isUpdate)
        {
            var processedAddresses = new List<Address>();

            foreach (var address in addresses)
            {
                var sameAddress = await _addressRepository.CheckIfAddresAlreadyExists(address.ZipCode, address.Street);

                if (sameAddress == null)
                {
                    processedAddresses.Add(address);

                    if (isUpdate) _addressRepository.AddAddress(address);

                    continue;
                }

                processedAddresses.Add(sameAddress);
            }

            return processedAddresses;
        }
    }
}
