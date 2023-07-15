using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities.OrderAgregate;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Orders
{

    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var order = await _orderService.CreateOrder(orderDto.DriverIdentity, orderDto.BasketId);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem Creating order"));

            return Ok(order);
        }



    }
}
