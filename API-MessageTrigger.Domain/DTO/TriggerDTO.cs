namespace API_MessageTrigger.Domain.DTO
{
    public class TriggerDTO
    {
        public string? InstanceName { get; set; }
        public ICollection<string> Numbers { get; set; }
        public ICollection<Messages> Messages { get; set; }
        //public string? MediaBase64 { get; set; }
    }

    public class Messages
    {
        public string? Type { get; set; }
        public string? Message { get; set; }
        public string? MediaType { get; set; }
        public string? Mimetype { get; set; }
        public string? Caption { get; set; }
        public string? FileName { get; set; }
        public string? Media { get; set; }
        public string? DataUrl { get; set; }
        
    }
}
