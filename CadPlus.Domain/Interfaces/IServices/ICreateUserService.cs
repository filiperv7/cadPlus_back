using CadPlus.Domain.Entities;

namespace CadPlus.Domain.Interfaces.IServices
{
    public interface ICreateUserService
    {
        Task<bool> CreateUser(User user, int idProfile, List<int> profiles);
    }
}
