

namespace Core.Entities.OrderAgregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {

        }

        public OrderItem(TrafficFineItemOrdered itemOrdered, decimal price)
        {

            ItemOrdered = itemOrdered;
            Price = price;
        }

        public TrafficFineItemOrdered ItemOrdered { get; set; }
        public decimal Price { get; set; }
    }
}
