using CadPlus.Domain.Entities;
using CadPlus.Domain.Enums;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Domain.Interfaces.IServices;

namespace CadPlus.Application.Services
{
    public class EditUserService : IEditUserService
    {
        private readonly IUserRepository _userRepository;

        public EditUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> EvolvePatient(Guid id, HealthStatus status, List<int> profiles)
        {
            if (profiles == null && (!profiles.Contains(2) || !profiles.Contains(3)))
            {
                throw new UnauthorizedAccessException("Somente Médicos(as) ou Enfermeiros(as) têm permissão para evoluir um Paciente.");
            }

            User userFounded = await _userRepository.GetById(id);

            if (userFounded == null) return false;

            userFounded.SetHealthStatus(status);

            await _userRepository.Update(userFounded);

            return true;
        }
    }
}
