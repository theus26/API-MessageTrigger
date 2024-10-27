namespace API_MessageTrigger.Domain.DTO
{
    public class TriggerDTO
    {
        public string? InstanceName { get; set; }
        public ICollection<string> Numbers { get; set; }
        public ICollection<Messages> Messages { get; set; }
        public string? MediaBase64 { get; set; }
    }

    public class Messages
    {
        public string type { get; set; }
        public string Message { get; set; }
        
    }
}
