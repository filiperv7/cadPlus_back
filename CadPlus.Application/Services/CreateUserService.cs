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
        private readonly IAddressService _addressService;

        public CreateUserService(IUserRepository userRepository, IAddressService addressService)
        {
            _userRepository = userRepository;
            _addressService = addressService;
        }

        public async Task<bool> CreateUser(User user, int idProfile, List<int> profiles)
        {
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

            user.SetAddresses(await _addressService.HandleWithAddresses(user.Addresses));

            await _userRepository.Create(user);

            return true;
        }
    }
}
