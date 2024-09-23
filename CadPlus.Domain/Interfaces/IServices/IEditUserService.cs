using CadPlus.Domain.Entities;
using CadPlus.Domain.Enums;

namespace CadPlus.Domain.Interfaces.IServices
{
    public interface IEditUserService
    {

        public Task<bool> EditUser(User user, List<Guid> addressesExcluded, List<int> requestUserProfiles, Guid requestUserId);

        Task<bool> EvolvePatient(Guid id, HealthStatus status, List<int> profiles);

        Task CheckInPatientWithExistingUser(Guid id, List<int> profiles);
    }
}
