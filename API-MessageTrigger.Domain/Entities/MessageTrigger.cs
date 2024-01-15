namespace API_MessageTrigger.Domain.Entities
{
    public class MessageTrigger : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string NameInstance { get; set; }
    }
}
