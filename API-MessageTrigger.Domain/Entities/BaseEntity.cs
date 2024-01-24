namespace API_MessageTrigger.Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
