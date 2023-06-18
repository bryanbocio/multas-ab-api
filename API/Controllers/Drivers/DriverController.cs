using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities.Driver;
using Core.Interfaces.GenericRepository;
using Core.Interfaces.UnitOfWork;
using Core.Specification;
using Core.Specification.Parameters.Driver;
using Microsoft.AspNetCore.Authorization;
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


        [HttpGet]
        [Authorize(Roles = "ADMIN, AGENT")]
        public async Task<ActionResult<Pagination<DriverToReturnDto>>> GetDrivers([FromQuery] DriverSpecificationParameters parameter)
        {
            var specifications = new DriverSpecification(parameter);

            var countSpecification = specifications;

            var totalItem = await _unitOfWork.Repository<Driver>().CountAsync(countSpecification);

            var drivers = await _unitOfWork.Repository<Driver>().ListAsync(specifications);

            var data = _mapper.Map<IReadOnlyList<Driver>, IReadOnlyList<DriverToReturnDto>>(drivers);

            return Ok(new Pagination<DriverToReturnDto>(parameter.PageIndex, parameter.PageSize, totalItem, data));
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
        [Authorize(Roles = "ADMIN, AGENT")]
        public async Task<ActionResult<DriverToReturnDto>> GetDriver(string id)
        {
            var specification = new DriverSpecification(id);
            
            var driver= await _unitOfWork.Repository<Driver>().GetEntityWithSpecification(specification);
           
            if(driver==null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Driver,DriverToReturnDto>(driver));
        }

    }
}
