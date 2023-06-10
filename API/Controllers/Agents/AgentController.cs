using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities.Agent;
using Core.Interfaces.UnitOfWork;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Agents
{
    public class AgentController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AgentController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }

        [HttpPost]
        public async Task<ActionResult> CreateAgent(AgentDto agent)
        {
            var agentForRegistering= _mapper.Map<AgentDto, Agent>(agent);

            _unitOfWork.Repository<Agent>().Add(agentForRegistering);

            var result =await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problems creating a Agent"));

            return Ok(new ApiResponse(201));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AgentToReturn>> GetDriver(string id)
        {
            var specification = new AgentSpecification(id);

            var agent = await _unitOfWork.Repository<Agent>().GetEntityWithSpecification(specification);

            if (agent == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Agent, AgentToReturn>(agent));
        }

    }
}
