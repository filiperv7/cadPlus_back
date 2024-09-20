namespace CadPlus.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public bool Excluded { get; set; } = false;

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? ExclusionDate { get; set; }
    }
}
