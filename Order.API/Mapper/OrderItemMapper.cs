using Order.API.Models.Entities;
using Order.API.ViewModels;

namespace Order.API.Mapper
{
    public static class OrderItemMapper
    {
        public static OrderItem MapToOrderItem(CreateOrderItemVM createOrderItem)
        {
            return new OrderItem
            {
                ProductId = createOrderItem.ProductId,
                Count = createOrderItem.Count,
                Price = createOrderItem.Price
            };
        }
    }
}
