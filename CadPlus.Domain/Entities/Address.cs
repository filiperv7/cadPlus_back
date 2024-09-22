using CadPlus.Domain.Common;
using System.Text.RegularExpressions;

namespace CadPlus.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address()
        {
            Id = Guid.NewGuid();
            Users = new List<User>();
        }

        public Address(string street, string city, string state, string zipCode)
        {
            Id = Guid.NewGuid();
            Street = street;
            City = city;
            State = state;
            ZipCode = Regex.Replace(zipCode, "[^0-9]", "");

            Users = new List<User>();
        }
        
        public string Street { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string ZipCode { get; set; }
        
        public List<User> Users { get; set; }
    }
}
