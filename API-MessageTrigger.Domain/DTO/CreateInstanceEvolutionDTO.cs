using Newtonsoft.Json;

namespace API_MessageTrigger.Domain.DTO
{
    public class CreateInstanceEvolutionDTO
    {
        [JsonProperty("instanceName")]
        public string InstanceName { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("qrcode")]
        public bool Qrcode { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
    }

}

