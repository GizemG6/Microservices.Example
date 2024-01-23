using Microsoft.AspNetCore.Mvc;
using Order.API.ViewModels;
using Order.API.Models.Entities;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderVM createOrder)
        {
            Order.API.Models.Entities.Order order = new()
            {
                OrderId = Guid.NewGuid(),
                BuyerID = createOrder.BuyerID,
                CreatedDate = DateTime.Now,
                OrderStatus = Models.Enums.OrderStatus.Suspend
            };

            order.OrderItems = createOrder.OrderItems.Select(oi => new OrderItem
            {
                Count = oi.Count,
                Price = oi.Price,
                ProductId = oi.ProductId,
            }).ToList();

            order.TotalPrice = createOrder.OrderItems.Sum(oi => oi.Price);

            return Ok();
        }
    }
}
