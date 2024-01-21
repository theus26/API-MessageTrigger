namespace API_MessageTrigger.Domain.DTO
{
    public class ResponseInstanceCreateDTO
    {
        public Qrcode qrcode { get; set; }
    }

    public class Qrcode
    {
        public string base64 { get; set; }
        public int count { get; set; }
    }

}

