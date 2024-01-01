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
        public RequestEvolutionApi(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public void CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendMessageWhatsapp(SendMessageEvolutionDTO sendMessageEvolution)
        {
            try
            {
                var UrlEvolution = SetUrl(sendMessageEvolution?.MediaMessage?.MediaType);

                //Iniciando a request
                var client = _httpClientFactory.CreateClient();
                try
                {
                    // Serializa o objeto para formato JSON
                    var requestBodyJson = JsonConvert.SerializeObject(sendMessageEvolution);

                    //Adiciona Header
                    client.DefaultRequestHeaders.Add("apikey", "B6D711FCDE4D4FD5936544120E713976");
                    // Cria o conteúdo da requisição com o corpo (body) em formato JSON
                    var httpContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(UrlEvolution, httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }

                    return false;
                }
                catch
                {
                    throw;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }

        }

        private string SetUrl(string? mediaType)
        {
            var Url = _configuration.GetSection("Urls").GetSection("UrlEvolutionApi").Value;

            if (mediaType is null)
            {
                return Url = $"{Url}/message/sendText/teste";
            }
            else
            {
                return Url = $"{Url}//message/sendMedia/teste";
            }
        }
    }
}
