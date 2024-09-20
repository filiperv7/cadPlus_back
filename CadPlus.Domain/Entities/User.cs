using CadPlus.Domain.Common;
using CadPlus.Domain.Enums;
using System.Text.RegularExpressions;

namespace CadPlus.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Id = Guid.NewGuid();
            Addresses = new List<Address>();
            Profiles = new List<Profile>();
            CreationDate = DateTime.UtcNow;
        }

        public User(
            string cpf,
            string name,
            string email,
            string password,
            string phone,
            HealthStatus healthStatus,
            List<Address> addresses = null,
            List<Profile> profiles = null)
        {
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            if (!IsValidCPF(cpf))
                throw new ArgumentException("CPF inválido.");
            if (!IsValidPassword(password))
                throw new ArgumentException("A senha deve ter mais de 8 caracteres, incluindo letras maiúsculas, minúsculas e caracteres especiais.");

            Id = Guid.NewGuid();
            CPF = cpf;
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            HealthStatus = healthStatus;

            Addresses = addresses ?? new List<Address>();
            Profiles = profiles ?? new List<Profile>();
            CreationDate = DateTime.UtcNow;

            Excluded = false;
        }

        public string CPF { get; private set; }
        
        public string Name { get; private set; }
        
        public List<Address> Addresses { get; private set; }
        
        public string Email { get; private set; }
        
        public string Password { get; private set; }
        
        public string Phone { get; private set; }
        
        public HealthStatus HealthStatus { get; private set; }

        public List<Profile> Profiles { get; private set; }

        public void SetPassword(string password)
        {
            if (!IsValidPassword(password))
                throw new ArgumentException("A senha deve ter mais de 8 caracteres, incluindo letras maiúsculas, minúsculas e caracteres especiais.");
            this.Password = password;
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;

            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperCase && hasLowerCase && hasSpecialChar;
        }

        private bool IsValidCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;

            if (cpf.Length != 11) return false;

            if (cpf.All(c => c == cpf[0])) return false;

            int[] multiplicadores1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
