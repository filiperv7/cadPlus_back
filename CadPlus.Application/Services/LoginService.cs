using CadPlus.Application.Helpers;
using CadPlus.Domain.Entities;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Domain.Interfaces.IServices;

namespace CadPlus.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> Login(string email, string password)
        {
            User user = await _userRepository.GetUserByEmail(email);

            if (user == null || !PasswordHelper.VerifyPassword(password, user.Password))
            {
                return null;
            }

            return _tokenService.GenerateToken(user);
        }
    }
}
