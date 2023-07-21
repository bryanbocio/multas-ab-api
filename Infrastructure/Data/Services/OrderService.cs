using Core.Entities.OrderAgregate;
using Core.Entities.TrafficFine;
using Core.Interfaces.Reporitories.BasketRepository;
using Core.Interfaces.Services;
using Core.Interfaces.UnitOfWork;
using Core.Specification;
using Infrastructure.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        public async Task<Order> CreateOrder(string driverId, string basketId)
        {

            var basket = await _basketRepository.GetBasketAsync(basketId);

            var itemList = new List<OrderItem>();

            foreach(var item in basket.Items)
            {
                var trafficFine= await _unitOfWork.Repository<TrafficFine>().GetByIdAsync(item.TrafficFineId);

                var trafficFineOrdered = new TrafficFineItemOrdered(trafficFine.Id, trafficFine.Reason);

                string codeReason = UtilsTrafficFineReasons.GetReasonCode(trafficFine.Reason);

                var reason = await _unitOfWork.Repository<TrafficFineReason>().GetEntityWithSpecification(new TrafficFineReasonSpecification(codeReason));
                
                var orderItem = new OrderItem(trafficFineOrdered, reason.Price);

                itemList.Add(orderItem);
            }

            var subTotal = itemList.Sum(item => item.Price);

            var order = new Order(itemList, driverId, subTotal);

            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            await _basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public Task<Order> GetOrderByIdAsync(int id, string driverId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string driverId)
        {
            throw new NotImplementedException();
        }

    }
}
