
using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Entities;
using AutoMapper;

namespace API_MessageTrigger.Infra.CrossCutting.AutoMapper

{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<MessageTrigger, CreateInstanceEvolutionDTO>().ReverseMap();
        }
    }
}
