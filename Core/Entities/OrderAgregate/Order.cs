

namespace Core.Entities.OrderAgregate
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }

        public Order(IReadOnlyList<OrderItem> orderItems,
                     string driverId,
                     decimal subTotal)
        {
            OrderItems = orderItems;
            DriverId = driverId;
            SubTotal= subTotal;
        }

        public string DriverId { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.UtcNow;
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.PaymentRecevied;
        public string? PaymentIntentId { get; set; }

    }
}
