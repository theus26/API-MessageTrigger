using Newtonsoft.Json;

namespace API_MessageTrigger.Domain.DTO
{
    public class SendMessageEvolutionDTO
    {
        [JsonProperty("number")]
        public string Number { get; set; }
        public Options Options { get; set; }
        [JsonProperty("mediaMessage")]
        public MediaMessage? MediaMessage { get; set; }
        [JsonProperty("textMessage")]
        public Textmessage TextMessage { get; set; }
    }

    public class Options
    {
        public int Delay { get; set; }
        public string Presence { get; set; }
        public bool LinkPreview { get; set; }
    }

    public class Textmessage
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
    public class MediaMessage
    {
        [JsonProperty("mediatype")]
        public string? MediaType { get; set; }
        [JsonProperty("caption")]
        public string? Caption { get; set; }
        [JsonProperty("media")]
        public string Base64 { get; set; }
    }

    
}
