using CadPlus.Domain.Enums;

namespace CadPlus.Domain.Interfaces.IServices
{
    public interface IEditUserService
    {
        Task<bool> EvolvePatient(Guid id, HealthStatus status, List<int> profiles);

        Task CheckInPatientWithExistingUser(Guid id, List<int> profiles);
    }
}
