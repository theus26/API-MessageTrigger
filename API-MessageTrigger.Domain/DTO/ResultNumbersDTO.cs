namespace API_MessageTrigger.Domain.DTO
{
    public class ResultNumbersDTO
    {
        public ICollection<string>? NumberSucess { get; set; } = [];
        public ICollection<string>? NumberError { get; set; } = [];
    }
}
