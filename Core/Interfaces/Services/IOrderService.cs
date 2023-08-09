using Core.Entities.OrderAgregate;
using Core.Entities.TrafficFine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<List<TrafficFine>> CreateOrder(string driverId, string basketId);

        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string driverId);

        Task<Order> GetOrderByIdAsync(int id, string driverId);

    }
}
