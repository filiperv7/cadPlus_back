using CadPlus.Domain.Entities;

namespace CadPlus.Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetUsersByProfile(int idProfile);
        Task<User> GetById(Guid id);
        Task Create(User user);
        Task Update(User user);
        Task Delete(Guid id);
        Task<Profile> GetProfileById(int id);
        Task<bool> CheckIfEmailAlreadyUsed(string email);
        Task<bool> CheckIfCpfAlreadyUsed(string cpf);
    }
}
