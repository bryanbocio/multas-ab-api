using API.DTOs;
using AutoMapper;
using Core.Entities.Driver;
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


            CreateMap<Driver, DriverToReturnDto>()
                                .ForMember(driverDto=> driverDto.DriverIdentity,option=> option.MapFrom(driver=>driver.DriverId))
                                .ForMember(driverDto => driverDto.PhoneNumber, option => option.MapFrom(driver => driver.Number))
                                .ReverseMap();
            CreateMap<DriverDto, Driver>()
                                .ForMember(driver=> driver.DriverId, options=>options.MapFrom(driverDto=>driverDto.DriverIdentity))
                                .ForMember(driver => driver.Number, option => option.MapFrom(driverDto => driverDto.PhoneNumber));
        }
    }
}
