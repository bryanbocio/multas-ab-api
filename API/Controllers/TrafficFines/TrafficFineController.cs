using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities.TrafficFine;
using Core.Interfaces.GenericRepository;
using Core.Specification;
using Core.Specification.Parameters.TrafficFine;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TrafficFines
{
   
    public class TrafficFineController : BaseController
    {
        private readonly IGenericRepository<TrafficFine> _fineRepository;
        private readonly IMapper _mapper;

        public TrafficFineController(IGenericRepository<TrafficFine> fineRepository, IMapper mapper)
        {
            _fineRepository = fineRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<TrafficFineDto>>> GetTrafficFines([FromQuery] TrafficFineSpecificationParameters parameters)
        {
            var specifications = new TrafficFineWithDriverAndAgentSpecification(parameters);
            var countSpecification= new TrafficFineWithFiltersForCountingSpecification(parameters);
            var totalItems = await _fineRepository.CountAsync(countSpecification);
            var trafficFines= await _fineRepository.ListAsync(specifications);

            var data= _mapper.Map<IReadOnlyList<TrafficFine>, IReadOnlyList<TrafficFineDto>>(trafficFines);

            return Ok(new Pagination<TrafficFineDto>(parameters.PageIndex, parameters.PageSize, totalItems, data));
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrafficFine>> GetTrafficFine(int id)
        {
            var trafficFine = await _fineRepository.GetByIdAsync(id);

            if(Object.Equals(trafficFine, null)) return NotFound(new ApiResponse(404));

            return Ok(trafficFine);
        }
    }
}
