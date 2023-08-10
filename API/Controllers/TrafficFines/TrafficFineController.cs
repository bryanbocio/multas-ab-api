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
        private static IList<TrafficFineReason> trafficFineReasonsCache= new List<TrafficFineReason>();

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
        public async Task<IList<TrafficFineReason>> GetTrafficFinesReasons()
        {
           


            List<TrafficFineReason> lstReasons= new  List<TrafficFineReason>();

            TrafficFineReason reason1= new TrafficFineReason(1, "AA1 Se pasó un semáforo en rojo", 2000);
            TrafficFineReason reason2= new TrafficFineReason(2, "AA2 Falta de respeto autoridad de tráncito", 2000);
            TrafficFineReason reason3= new TrafficFineReason(3, "AA3 Dobló en U donde no debía", 2000);
            TrafficFineReason reason4= new TrafficFineReason(4, "AA4 No respetó una señal de PARE", 2000);
            TrafficFineReason reason5= new TrafficFineReason(5, "AA5 Placa de automovil vencida", 2000);
            TrafficFineReason reason6= new TrafficFineReason(6, "AA6 Vehículo circulando sin marbete", 2000);
            TrafficFineReason reason7= new TrafficFineReason(7, "AA6 Vehículo con luces intermitentes no funcionales circulando", 2000);
            TrafficFineReason reason8= new TrafficFineReason(8, "Motorista circulando sin casco de protección", 2000);

            lstReasons.Add(reason1);
            lstReasons.Add(reason2);
            lstReasons.Add(reason3);
            lstReasons.Add(reason4);
            lstReasons.Add(reason5);
            lstReasons.Add(reason6);
            lstReasons.Add(reason7);
            lstReasons.Add(reason8);



          

            return lstReasons;
        }

    }
}
