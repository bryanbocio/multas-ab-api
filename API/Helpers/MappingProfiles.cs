using API.DTOs;
using AutoMapper;
using Core.Entities.Agent;
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
                                .ForMember(fineDto=> fineDto.AgentIdentity, options=> options.MapFrom(fine=> fine.Agent.AgentId));

            CreateMap<TrafficFineDto, TrafficFine >()
                                .ForPath(fine => fine.Driver.Name, options => options.MapFrom(fineDto => fineDto.DriverName))
                                .ForPath(fine => fine.Driver.DriverId, options => options.MapFrom(fineDto => fineDto.DriverIdentity))
                                .ForPath(fine => fine.Driver.Number, options => options.MapFrom(fineDto => fineDto.DriverPhoneNumber))
                                .ForPath(fine => fine.Agent.AgentId, options => options.MapFrom(fineDto => fineDto.AgentIdentity));


            CreateMap<Driver, DriverToReturnDto>()
                                .ForMember(driverDto=> driverDto.DriverIdentity,option=> option.MapFrom(driver=>driver.DriverId))
                                .ForMember(driverDto => driverDto.PhoneNumber, option => option.MapFrom(driver => driver.Number))
                                .ReverseMap();

            CreateMap<DriverDto, Driver>()
                                .ForMember(driver=> driver.DriverId, options=>options.MapFrom(driverDto=>driverDto.DriverIdentity))
                                .ForMember(driver => driver.Number, option => option.MapFrom(driverDto => driverDto.PhoneNumber));

            CreateMap<Agent, AgentToReturn>()
                                .ForMember(agentDto => agentDto.AgentIdentity, option => option.MapFrom(driver => driver.AgentId))
                                .ForMember(agentDto => agentDto.PhoneNumber, option => option.MapFrom(driver => driver.Number))
                                .ReverseMap();

            CreateMap<AgentDto, Agent>()
                               .ForMember(agent => agent.AgentId, options => options.MapFrom(driverDto => driverDto.AgentIdentity))
                               .ForMember(agent => agent.Number, option => option.MapFrom(driverDto => driverDto.PhoneNumber));
        }
    }
}
