using CadPlus.Domain.Entities;
using CadPlus.Domain.Enums;
using CadPlus.Domain.Interfaces.IRepositories;
using CadPlus.Domain.Interfaces.IServices;
using Profile = CadPlus.Domain.Entities.Profile;

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
                throw new UnauthorizedAccessException("Somente Médicos(as) ou Enfermeiros(as) têm permissão para evoluir um Paciente.");

            User userFounded = await _userRepository.GetById(id);

            if (userFounded == null) return false;

            userFounded.SetHealthStatus(status);

            await _userRepository.Update(userFounded);

            return true;
        }

        public async Task CheckInPatientWithExistingUser(Guid id, List<int> profiles)
        {
            if (profiles == null || !profiles.Contains(1))
                throw new UnauthorizedAccessException("Você não tem permissão para criar um novo usuário.");

            User user = await _userRepository.GetById(id);

            if (user == null) throw new KeyNotFoundException("Usuário não encontrado");

            var isAlreadyPatient = user.Profiles.Any(p => p.Id == 4);

            if (isAlreadyPatient) throw new InvalidOperationException("Este usuário já é um paciente.");

            user.SetHealthStatus(HealthStatus.Unknown);

            Profile profile = await _userRepository.GetProfileById(4);
            user.Profiles.Add(profile);

            await _userRepository.Update(user);
        }
    }
}
