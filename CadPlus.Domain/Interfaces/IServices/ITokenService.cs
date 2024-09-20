using CadPlus.Domain.Entities;

namespace CadPlus.Domain.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
