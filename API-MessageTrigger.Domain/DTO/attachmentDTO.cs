﻿using Microsoft.AspNetCore.Http;

namespace API_MessageTrigger.Domain.DTO
{
    public class AttachmentDTO
    {
        public IFormFile File { get; set; }
        public IFormFile ImageBase64 { get; set; }
        public string Text { get; set; }
    }
}
