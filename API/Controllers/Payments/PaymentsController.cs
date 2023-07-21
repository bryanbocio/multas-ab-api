using Core.Entities.Basket;
using Core.Interfaces.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Payments
{
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            return await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        }

        
    }
}
