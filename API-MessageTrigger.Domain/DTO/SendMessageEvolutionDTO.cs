﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace API_MessageTrigger.Domain.DTO
{
    public class SendMessageEvolutionDTO
    {
        [JsonProperty("number")]
        public string Number { get; set; }
        public Options Options { get; set; }
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
        public string? MediaType { get; set; }
        public string? Caption { get; set; }
        [JsonPropertyName("Media")]
        public string Base64 { get; set; }
    }

    
}
