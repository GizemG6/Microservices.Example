using Microsoft.AspNetCore.Mvc;
using Order.API.ViewModels;
using Order.API.Models.Entities;
using Order.API.Models;
using Microsoft.EntityFrameworkCore;
using Order.API.Mapper;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        readonly OrderAPIDbContext _context;

        public OrdersController(OrderAPIDbContext context)
        {
            _context = context;
        }

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

            order.TotalPrice = createOrder.OrderItems.Sum(oi => (oi.Price * oi.Count));

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
