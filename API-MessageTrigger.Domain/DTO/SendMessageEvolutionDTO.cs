using Newtonsoft.Json;

namespace API_MessageTrigger.Domain.DTO
{
    public class SendMessageEvolutionDTO
    {
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("mediatype")]
        public string? mediatype { get; set; }
        [JsonProperty("mimetype")]
        public string? mimetype { get; set; }
        [JsonProperty("caption")]
        public string caption { get; set; }
        [JsonProperty("media")] 
        public string? media { get; set; }
        [JsonProperty("fileName")]
        public string? fileName { get; set; }
    }
}
