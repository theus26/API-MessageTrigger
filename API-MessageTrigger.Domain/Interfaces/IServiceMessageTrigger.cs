using API_MessageTrigger.Domain.DTO;

namespace API_MessageTrigger.Domain.Interfaces
{
    public interface IServiceMessageTrigger
    {
        Task<ResultNumbersDTO> ProcessMessage(TriggerDTO triggerDto);
        bool CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution);
        ConnectInstanceDTO? ConnectInstance(string instanceName);
        List<InstancesDTO>? FetchInstance(string accountId);
        void LogoutInstances(string instanceName);
        void DeleteInstances(string instanceName);
    }
}
