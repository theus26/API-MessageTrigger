namespace API_MessageTrigger.Domain.DTO
{
    public class ResponseInstanceCreateDTO
    {
        public Qrcode Qrcode { get; set; }
    }

    public class Qrcode
    {
        public string Base64 { get; set; }
        public int Count { get; set; }
    }

}

