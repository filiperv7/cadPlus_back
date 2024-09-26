using CadPlus.Domain.Entities;

namespace CadPlus.Domain.Interfaces.IServices
{
    public interface IFindUsersServices
    {
        Task<IEnumerable<User>> FindUsersByProfile(int idProfile);
        Task<User> FindUserById(Guid id);

        Task<User> GetInfosOfLoggedUser(Guid requestUserId);
    }
}
