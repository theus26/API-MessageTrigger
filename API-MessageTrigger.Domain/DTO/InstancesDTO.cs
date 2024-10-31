using Newtonsoft.Json;

namespace API_MessageTrigger.Domain.DTO
{
    public class InstancesDTO
    {
        [JsonProperty("name")]
        public string InstanceName { get; set; }
        [JsonProperty("id")]
        public string InstanceId { get; set; }
        [JsonProperty("connectionStatus")]
        public string Status { get; set; }
        [JsonProperty("ownerJid")]
        public string Owner { get; set; }
        [JsonProperty("profileName")]
        public string ProfileName { get; set; }
        [JsonProperty("profilePicUrl")]
        public string ProfilePictureUrl { get; set; }
    }
}
