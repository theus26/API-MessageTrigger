using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

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



                var response = await client.PostAsync(UrlEvolution);

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
                return Url = $"{Url}/message/sendText/instance (Instance name)";
            }
            else
            {
                return Url = $"{Url}//message/sendMedia/instance (Instance name)";
            }
        }
    }
}
