using Core.Entities.OrderAgregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(int orderId, string driverId ) : base( order=> order.Id==orderId && order.DriverId==driverId)
        {
            AddInclude(order => order.Status);
            AddInclude(order => order.OrderItems);
        }

        public OrderSpecification(string driverId) : base(order => order.DriverId == driverId)
        {
            AddInclude(order => order.Status);
            AddInclude(order => order.OrderItems);
        }



    }
}
