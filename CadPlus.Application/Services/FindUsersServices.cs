using CadPlus.Domain.Entities;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Domain.Interfaces.IServices;

namespace CadPlus.Application.Services
{
    public class FindUsersServices : IFindUsersServices
    {
        private readonly IUserRepository _userRepository;

        public FindUsersServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> FindUsersByProfile(int idProfile)
        {
            return await _userRepository.GetUsersByProfile(idProfile);
        }

        public async Task<User> FindUserById(Guid id)
        {
            return await _userRepository.GetById(id);
        }
    }
}
