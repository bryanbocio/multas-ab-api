using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities.Agent;
using Core.Entities.Driver;
using Core.Entities.TrafficFine;
using Core.Interfaces.Services;
using Core.Interfaces.UnitOfWork;
using Core.Specification;
using Core.Specification.Parameters.TrafficFine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TrafficFines
{
   
    public class TrafficFineController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrafficFineService _trafficFineService;
        private readonly IMapper _mapper;

        public TrafficFineController(IMapper mapper,IUnitOfWork unitOfWork, ITrafficFineService trafficFineService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _trafficFineService = trafficFineService;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<TrafficFineToReturnDto>>> GetTrafficFines([FromQuery] TrafficFineSpecificationParameters parameters)
        {

            if(parameters.IdentityDriver != null)
            {
                var driver = await _unitOfWork.Repository<Driver>().GetEntityWithSpecification(new DriverSpecification(parameters.IdentityDriver));
                if (driver == null) return BadRequest(new ApiResponse(404));
                parameters.DriverId = driver.Id;
            }

            if (parameters.IdentityAgent != null)
            {
                var agent = await _unitOfWork.Repository<Agent>().GetEntityWithSpecification(new AgentSpecification(parameters.IdentityAgent));
                if (agent == null) return BadRequest(new ApiResponse(404));
                parameters.AgentId = agent.Id;
            }

            var specifications = new TrafficFineWithDriverAndAgentSpecification(parameters);

            var countSpecification= specifications;
            
            var totalItems = await _unitOfWork.Repository<TrafficFine>().CountAsync(countSpecification);
            
            var trafficFines= await _unitOfWork.Repository<TrafficFine>().ListAsync(specifications);

            var data= _mapper.Map<IReadOnlyList<TrafficFine>, IReadOnlyList<TrafficFineToReturnDto>>(trafficFines);

            return Ok(new Pagination<TrafficFineToReturnDto>(parameters.PageIndex, parameters.PageSize, totalItems, data));
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrafficFine>> GetTrafficFine(int id)
        {

            var parameter = new TrafficFineSpecificationParameters { TrafficFineId = id };
            var specifications = new TrafficFineWithDriverAndAgentSpecification(parameter); 
            var trafficFine = await _unitOfWork.Repository<TrafficFine>().GetEntityWithSpecification(specifications);

            if(Object.Equals(trafficFine, null)) return NotFound(new ApiResponse(404));

            return Ok(trafficFine);
        }

        [Authorize(Roles ="ADMIN, AGENT")]
        [HttpPost]
        public async Task<ActionResult<TrafficFine>> CreateTrafficFine(TrafficFineDto trafficFineDto)
        {
            if (_unitOfWork.Repository<Driver>().GetEntityWithSpecification(new DriverSpecification(trafficFineDto.DriverIdentity)) == null)
            {
                return BadRequest(new ApiResponse(404, "The driver does not exists"));
            }

            var trafficFineCreated=await _trafficFineService.CreateTrafficFine
                (
                 trafficFineDto.DriverIdentity,
                 trafficFineDto.AgentIdentity,
                 trafficFineDto.CarPlate,
                 trafficFineDto.Reason,
                 trafficFineDto.Comment,
                 trafficFineDto.Latitude,
                 trafficFineDto.Longitude,
                 trafficFineDto.DateCreated
                );

            if (trafficFineCreated == null) return BadRequest(new ApiResponse(400, "Problems creating traffic fine"));

            return Ok(trafficFineCreated);
        }

        [HttpGet("reasons")]
        public ActionResult<List<TrafficFineReasonDto>> GetTrafficFinesReasons()
        {
            List<TrafficFineReasonDto> lstReason = new List<TrafficFineReasonDto>();

            if(lstReason.Count <= 0)
            {
                TrafficFineReasonDto trafficFineReason1 = new TrafficFineReasonDto { Id = 1, Reason = "Pasó semáforo en rojo", Price = 100 };
                TrafficFineReasonDto trafficFineReason2 = new TrafficFineReasonDto { Id = 1, Reason = "Falta de respeto autoridad de tráncito", Price = 700 };
                TrafficFineReasonDto trafficFineReason3 = new TrafficFineReasonDto { Id = 1, Reason = "Dobló en U donde no debía", Price = 400 };
                TrafficFineReasonDto trafficFineReason4 = new TrafficFineReasonDto { Id = 1, Reason = "No respetó la señal de pare", Price = 400 };

                lstReason.Add(trafficFineReason1);
                lstReason.Add(trafficFineReason2);
                lstReason.Add(trafficFineReason3);
                lstReason.Add(trafficFineReason4);
            }

            return Ok(lstReason) ;
        }



    }
}
