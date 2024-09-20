namespace CadPlus.Domain.Interfaces.IServices
{
    public interface ILoginService
    {
        Task<string> Login(string email, string password);
    }
}
