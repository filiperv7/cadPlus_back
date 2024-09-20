using CadPlus.Domain.Entities;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Domain.Interfaces.IServices;
using CadPlus.Application.Helpers;

namespace CadPlus.Application.Services
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IUserRepository _userRepository;

        public CreateUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            Profile profile = await _userRepository.GetProfileById(idProfile);
            user.Profiles.Add(profile);

            await _userRepository.Create(user);

            return true;
        }
    }
}
