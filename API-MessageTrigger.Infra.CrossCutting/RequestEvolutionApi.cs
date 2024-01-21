using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace API_MessageTrigger.Infra.CrossCutting
{
    public class RequestEvolutionApi : IRequestEvolutionApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private string _instance = "Create";
        public RequestEvolutionApi(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<string?> CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution)
        {
            string urlEvolution = SetUrl(_instance);
            var client = _httpClientFactory.CreateClient();

            try
            {
                string requestBodyJson = SerializeObjectToJson(createInstanceEvolution);
                AddApiKeyHeader(client);

                var httpContent = CreateJsonHttpContent(requestBodyJson);

                var response = await client.PostAsync(urlEvolution, httpContent);

                string content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return ExtractBase64FromResponse(content);
                }

                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        public async Task<bool> SendMessageWhatsapp(SendMessageEvolutionDTO sendMessageEvolution)
        {
            try
            {
                string urlEvolution = SetUrl(sendMessageEvolution?.MediaMessage?.MediaType);
                var client = _httpClientFactory.CreateClient();
                string requestBodyJson = JsonConvert.SerializeObject(sendMessageEvolution);
                AddApiKeyHeader(client);
                var httpContent = CreateJsonHttpContent(requestBodyJson);
                var response = await client.PostAsync(urlEvolution, httpContent);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }

        }

        private string SetUrl(string? mediaType)
        {
            var apiUrl = _configuration.GetSection("Urls:UrlEvolutionApi").Value;

            if (mediaType == _instance)
            {
                return $"{apiUrl}/instance/create";
            }

            if (mediaType is null)
            {
                return $"{apiUrl}/message/sendText/message";
            }

            return $"{apiUrl}/message/sendMedia/message";
        }

        private string SerializeObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private void AddApiKeyHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("apikey", "B6D711FCDE4D4FD5936544120E713976");
        }

        private StringContent CreateJsonHttpContent(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private string ExtractBase64FromResponse(string content)
        {
            var deserialized = JsonConvert.DeserializeObject<ResponseInstanceCreateDTO>(content);
            return deserialized.qrcode.base64;
        }
    }
}
