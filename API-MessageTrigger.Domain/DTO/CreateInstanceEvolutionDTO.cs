using Newtonsoft.Json;

namespace API_MessageTrigger.Domain.DTO
{
    public class CreateInstanceEvolutionDTO
    {
        [JsonProperty("instanceName")]
        public string InstanceName { get; set; }
    }

}

