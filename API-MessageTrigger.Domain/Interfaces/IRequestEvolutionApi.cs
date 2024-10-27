using API_MessageTrigger.Domain.DTO;

namespace API_MessageTrigger.Domain.Interfaces
{
    public interface IRequestEvolutionApi
    {
        Task<bool> CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution);
        Task<ConnectInstanceDTO?> ConnectInstance(string instanceName);
        Task<bool> SendMessageWhatsapp(SendMessageEvolutionDTO sendMessageEvolution, string instanceName);
        Task<List<InstancesDTO>?> FetchInstances();
        void LogoutInstances(string instanceName);
        void DeleteInstances(string instanceName);
    }
}
