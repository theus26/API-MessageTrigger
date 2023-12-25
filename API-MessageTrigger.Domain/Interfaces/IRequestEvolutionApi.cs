using API_MessageTrigger.Domain.DTO;

namespace API_MessageTrigger.Domain.Interfaces
{
    public interface IRequestEvolutionApi
    {
        void CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution);
        void SendMessageWhatsapp(SendMessageEvolutionDTO sendMessageEvolution);
    }
}
