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
        public async Task<bool> CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution)
        {
            var urlEvolution = SetUrl(_instance);
            var client = _httpClientFactory.CreateClient("EvolutionAPI");
          
            try
            {
                var requestBodyJson = SerializeObjectToJson(createInstanceEvolution);
                AddApiKeyHeader(client);
                var httpContent = CreateJsonHttpContent(requestBodyJson);
                var response = await client.PostAsync("instance/create", httpContent);
                return response.IsSuccessStatusCode;

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        public async Task<bool> SendMessageWhatsapp(SendMessageEvolutionDTO sendMessageEvolution, string instanceName)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("EvolutionAPI");
                string requestBodyJson = SerializeObjectToJson(sendMessageEvolution);
                AddApiKeyHeader(client);
                var httpContent = CreateJsonHttpContent(requestBodyJson);
                if (sendMessageEvolution?.mediatype is null)
                {
                    var response = await client.PostAsync($"message/sendText/{instanceName}", httpContent);
                    return response.IsSuccessStatusCode;
                }
                else
                {
                    var response = await client.PostAsync($"message/sendMedia/{instanceName}", httpContent);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }

        }

        private string SetUrl(string? mediaType, string? instanceName = null)
        {
            var url = _configuration.GetSection("Evolution:UrlEvolutionApi").Value;

            if (mediaType == _instance)
            {
                return $"{url}/instance/create";
            }

            return mediaType is null ? $"{url}/message/sendText/{instanceName}" : $"{url}/message/sendMedia/{instanceName}";
        }

        public async Task<ConnectInstanceDTO?> ConnectInstance(string instanceName)
        {
            var url = _configuration.GetSection("Evolution:UrlEvolutionApi").Value;
            var client = _httpClientFactory.CreateClient("EvolutionAPI");

            try
            {
                AddApiKeyHeader(client);
                var response = await client.GetAsync($"instance/connect/{instanceName}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Não foi possivel realizar a request");

                var content = await response.Content.ReadAsStringAsync();
                var base64 = JsonConvert.DeserializeObject<ResponseInstanceCreateDTO>(content)?.Base64;
                return new ConnectInstanceDTO()
                {
                    base64 = base64 ?? string.Empty
                };


            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        public async Task<List<InstancesDTO>?> FetchInstances()
        {
            var url = _configuration.GetSection("Evolution:UrlEvolutionApi").Value;
            var client = _httpClientFactory.CreateClient("EvolutionAPI");

            try
            {
                AddApiKeyHeader(client);
                var response = await client.GetAsync("instance/fetchInstances");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Não foi possivel realizar a request");

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<InstancesDTO>>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        public async void LogoutInstances(string instanceName)
        {
            var url = _configuration.GetSection("Evolution:UrlEvolutionApi").Value;
            var client = _httpClientFactory.CreateClient("EvolutionAPI");

            try
            {
                AddApiKeyHeader(client);
                var response = await client.DeleteAsync($"instance/logout/{instanceName}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Não foi possivel realizar a request");

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        public async void DeleteInstances(string instanceName)
        {
            var url = _configuration.GetSection("Evolution:UrlEvolutionApi").Value;
            var client = _httpClientFactory.CreateClient("EvolutionAPI");

            try
            {
                AddApiKeyHeader(client);
                var response = await client.DeleteAsync($"instance/delete/{instanceName}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Não foi possivel realizar a request");
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        private static string SerializeObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private void AddApiKeyHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("apikey", "hxq4qJHh21goZCqi3rOnS8Hs59ooaDpo");
        }

        private static StringContent CreateJsonHttpContent(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
