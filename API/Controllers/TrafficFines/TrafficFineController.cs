using API.Errors;
using Core.Entities.TrafficFine;
using Core.Interfaces.GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TrafficFines
{
   
    public class TrafficFineController : BaseController
    {
        private readonly IGenericRepository<TrafficFine> _fineRepository;

        public TrafficFineController(IGenericRepository<TrafficFine> fineRepository)
        {
            _fineRepository = fineRepository;
        }

        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TrafficFine>>> GetAllTrafficFines()
        {
            return Ok(await _fineRepository.GetAllAsync());
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
