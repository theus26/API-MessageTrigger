using API_MessageTrigger.Domain.DTO;

namespace API_MessageTrigger.Domain.Interfaces
{
    public interface IRequestEvolutionApi
    {
        Task<string> CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution);
        Task<bool> SendMessageWhatsapp(SendMessageEvolutionDTO sendMessageEvolution, string instanceName);
    }
}
