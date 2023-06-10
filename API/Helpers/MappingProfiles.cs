using API.DTOs;
using AutoMapper;
using Core.Entities.TrafficFine;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TrafficFine, TrafficFineToReturnDto>()
                            .ForMember(fineDto=> fineDto.DriverName, options=> options.MapFrom(fine=> fine.Driver.Name))
                            .ForMember(fineDto=> fineDto.DriverIdentity, options=> options.MapFrom(fine=> fine.Driver.DriverId))
                            .ForMember(fineDto=> fineDto.DriverPhoneNumber, options=> options.MapFrom(fine=> fine.Driver.Number))
                            .ForMember(fineDto=> fineDto.AgentIdentity, options=> options.MapFrom(fine=> fine.Agent.Name));
        }
    }
}
