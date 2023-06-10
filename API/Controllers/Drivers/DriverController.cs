using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities.Driver;
using Core.Interfaces.GenericRepository;
using Core.Interfaces.UnitOfWork;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Drivers
{
    public class DriverController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DriverController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDriver(DriverDto driver)
        {
            var driverForRegistering= _mapper.Map<DriverDto,Driver>(driver);

            _unitOfWork.Repository<Driver>().Add(driverForRegistering);
            
            var result=await _unitOfWork.Complete();

            if (result<=0) return BadRequest(new ApiResponse(400));

            return Ok(new ApiResponse(201));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DriverToReturnDto>> GetDriver(string id)
        {
            var specification = new DriverSpecification(id);
            
            var driver= await _unitOfWork.Repository<Driver>().GetEntityWithSpecification(specification);
           
            if(driver==null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Driver,DriverToReturnDto>(driver));
        }

    }
}
