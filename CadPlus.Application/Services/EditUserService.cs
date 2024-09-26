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
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressService _addressService;

        public EditUserService(IUserRepository userRepository, IAddressRepository addressRepository, IAddressService addressService)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _addressService = addressService;
        }

        public async Task<bool> EditUser(User user, List<Guid> addressesExcluded, List<int> requestUserProfiles, Guid requestUserId)
        {
            if (requestUserProfiles == null || (!requestUserProfiles.Contains(1) && user.Id != requestUserId))
            {
                throw new UnauthorizedAccessException("Você não tem permissão para editar este usuário.");
            }

            if (!user.IsValidCPF(user.CPF))
                throw new ArgumentException("CPF inválido.");

            bool emailAlreadyUsed = await _userRepository.CheckIfEmailAlreadyUsed(user.Email, user.Id);
            if (emailAlreadyUsed == true) return false;

            bool cpfAlreadyUsed = await _userRepository.CheckIfCpfAlreadyUsed(user.CPF, user.Id);
            if (cpfAlreadyUsed == true) return false;

            var existingUser = await _userRepository.GetById(user.Id);
            if (existingUser == null) throw new KeyNotFoundException("Usuário não encontrado");

            existingUser.SetName(user.Name);
            existingUser.SetEmail(user.Email);
            existingUser.SetPhone(user.Phone);
            existingUser.SetCpf(user.CPF);
            existingUser.UpdateDate = DateTime.UtcNow;

            if (addressesExcluded.Count > 0) await _addressRepository.RemoveUserAddresses(user.Id, addressesExcluded);

            existingUser.SetAddresses(await _addressService.HandleWithAddresses(user.Addresses, true));

            await _userRepository.Update(existingUser);

            return true;
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
