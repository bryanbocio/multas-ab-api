using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities.Agent;
using Core.Entities.Driver;
using Core.Entities.TrafficFine;
using Core.Interfaces.GenericRepository;
using Core.Interfaces.UnitOfWork;
using Core.Specification;
using Core.Specification.Parameters.TrafficFine;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TrafficFines
{
   
    public class TrafficFineController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrafficFineController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<TrafficFineToReturnDto>>> GetTrafficFines([FromQuery] TrafficFineSpecificationParameters parameters)
        {
            var specifications = new TrafficFineWithDriverAndAgentSpecification(parameters);

            var countSpecification= new TrafficFineWithFiltersForCountingSpecification(parameters);
            
            var totalItems = await _unitOfWork.Repository<TrafficFine>().CountAsync(countSpecification);
            
            var trafficFines= await _unitOfWork.Repository<TrafficFine>().ListAsync(specifications);

            var data= _mapper.Map<IReadOnlyList<TrafficFine>, IReadOnlyList<TrafficFineToReturnDto>>(trafficFines);

            return Ok(new Pagination<TrafficFineToReturnDto>(parameters.PageIndex, parameters.PageSize, totalItems, data));
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrafficFine>> GetTrafficFine(int id)
        {
            var trafficFine = await _unitOfWork.Repository<TrafficFine>().GetByIdAsync(id);

            if(Object.Equals(trafficFine, null)) return NotFound(new ApiResponse(404));

            return Ok(trafficFine);
        }



  

    }
}
