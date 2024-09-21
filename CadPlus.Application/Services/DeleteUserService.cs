using CadPlus.Application.Helpers;
using CadPlus.Domain.Entities;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Domain.Interfaces.IServices;

namespace CadPlus.Application.Services
{
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteUser(Guid id, List<int> profiles)
        {
            if (profiles == null || !profiles.Contains(1))
            {
                throw new UnauthorizedAccessException("Você não tem permissão para criar um novo usuário.");
            }

            User user = await _userRepository.GetById(id);
            if (user == null) return false;

            user.Excluded = true;
            user.ExclusionDate = DateTime.UtcNow;

            await _userRepository.Update(user);

            return true;
        }
    }
}
