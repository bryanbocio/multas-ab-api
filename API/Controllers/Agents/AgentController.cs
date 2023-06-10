using API.Errors;
using Core.Entities.Agent;
using Core.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Agents
{
    public class AgentController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;

        public AgentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpPost]
        public async Task<ActionResult> CreateAgent(Agent agent)
        {
            _unitOfWork.Repository<Agent>().Add(agent);

            var result =await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problems creating a Agent"));

            return Ok();
        }

    }
}
