using Core.Entities.Basket;
using Core.Entities.TrafficFine;
using Core.Interfaces.Payment;
using Core.Interfaces.Reporitories.BasketRepository;
using Core.Interfaces.UnitOfWork;
using Core.Specification;
using Infrastructure.Data.Utils;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class PaymentService : IPaymentService
    {

        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfwork;
        private readonly IConfiguration _config;



        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _basketRepository = basketRepository;
            _unitOfwork = unitOfWork;
            _config = config;

        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);

            decimal totalAmount = await GetTotalAmoutOfTrafficFines(basket);

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)totalAmount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> {"card"}
                };

                intent= await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long) totalAmount
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdatedClientSecretAndPaymentIntent(basket);

            return basket;



        }

        private async Task<decimal> GetTotalAmoutOfTrafficFines(CustomerBasket basket)
        {

            decimal total = 0;

            foreach(var item in basket.Items)
            {
                var trafficFine = await _unitOfwork.Repository<TrafficFine>().GetByIdAsync(item.TrafficFineId);

                string codeReason = UtilsTrafficFineReasons.GetReasonCode(trafficFine.Reason);

                var reason = await _unitOfwork.Repository<TrafficFineReason>().GetEntityWithSpecification(new TrafficFineReasonSpecification(codeReason));

                total = total + reason.Price;
            }

            return total;
        }
    }
}
