namespace API_MessageTrigger.Domain.DTO
{
    public class InstancesDTO
    {
        public Instance Instance { get; set; }
    }

    public class Instance
    {
        public string InstanceName { get; set; }
        public string InstanceId { get; set; }
        public string Status { get; set; }
        public string ServerUrl { get; set; }
        public string ApiKey { get; set; }
        public string Owner { get; set; }
        public string ProfileName { get; set; }
        public string ProfileStatus { get; set; }
    }


}
