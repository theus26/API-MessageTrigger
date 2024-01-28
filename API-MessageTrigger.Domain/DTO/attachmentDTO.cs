using Microsoft.AspNetCore.Http;

namespace API_MessageTrigger.Domain.DTO
{
    public class AttachmentDTO
    {
        public IFormFile? File { get; set; }
        public IFormFile? MediaBase64 { get; set; }
        public string? Text { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
