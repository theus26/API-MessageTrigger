using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Interfaces;

namespace API_MessageTrigger.Infra.CrossCutting
{

    public class RequestEvolutionApi : IRequestEvolutionApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public void CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution)
        {
            throw new NotImplementedException();
        }

        public void SendMessageWhatsapp(SendMessageEvolutionDTO sendMessageEvolution)
        {
            throw new NotImplementedException();
        }
    }
}
