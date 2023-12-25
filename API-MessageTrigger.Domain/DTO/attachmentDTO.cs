using Microsoft.AspNetCore.Http;

namespace API_MessageTrigger.Domain.DTO
{
    public class attachmentDTO
    {
        public IFormFile File { get; set; }
    }
}
