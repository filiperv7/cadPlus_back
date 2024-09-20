namespace CadPlus.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
