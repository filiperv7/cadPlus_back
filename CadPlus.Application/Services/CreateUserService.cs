using CadPlus.Domain.Entities;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Domain.Interfaces.IServices;
using CadPlus.Application.Helpers;
using CadPlus.Domain.Enums;

namespace CadPlus.Application.Services
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

        public CreateUserService(IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        public async Task<bool> CreateUser(User user, int idProfile, string token)
        {
            var profiles = JwtHelper.GetProfilesFromToken(token);

            if (profiles == null || !profiles.Contains(1))
            {
                throw new UnauthorizedAccessException("Você não tem permissão para criar um novo usuário.");
            }

            bool emailAlreadyUsed = await _userRepository.CheckIfEmailAlreadyUsed(user.Email);
            if (emailAlreadyUsed == true) return false;

            bool cpfAlreadyUsed = await _userRepository.CheckIfCpfAlreadyUsed(user.CPF);
            if (cpfAlreadyUsed == true) return false;

            user.SetPassword(PasswordHelper.HashPassword(user.Password));

            if (idProfile == 4) user.SetHealthStatus(HealthStatus.Unknown);

            Profile profile = await _userRepository.GetProfileById(idProfile);
            user.Profiles.Add(profile);

            user.SetAddresses(await this.HandleWithAddresses(user.Addresses));

            await _userRepository.Create(user);

            return true;
        }

        private async Task<List<Address>> HandleWithAddresses(List<Address> addresses)
        {
            var processedAddresses = new List<Address>();

            foreach (var address in addresses)
            {
                var sameAddress = await _addressRepository.CheckIfAddresAlreadyExists(address.ZipCode, address.Street);

                if (sameAddress == null) 
                {
                    processedAddresses.Add(address);
                    continue;
                }

                processedAddresses.Add(sameAddress);
            }

            return processedAddresses;
        }
    }
}
