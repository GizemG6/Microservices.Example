using Microsoft.AspNetCore.Connections;
using Order.API.Models.Enums;

namespace Order.API.Models.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid BuyerID { get; set; }
        public long TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
