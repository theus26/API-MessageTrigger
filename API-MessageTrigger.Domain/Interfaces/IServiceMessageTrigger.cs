using API_MessageTrigger.Domain.DTO;

namespace API_MessageTrigger.Domain.Interfaces
{
    public interface IServiceMessageTrigger
    {
         Task<ResultNumbersDTO> ProcessMessage(AttachmentDTO attachment);

    }
}
