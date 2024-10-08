﻿using MassTransit;
using MongoDB.Driver;
using Shared.Events;
using Shared.Messages;
using Stock.API.Services;

namespace Stock.API.Consumer
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        IMongoCollection<Stock.API.Models.Entities.Stock> _stockCollection;

        public OrderCreatedEventConsumer(MongoDBService mongoDBService)
        {
            _stockCollection = mongoDBService.GetCollection<Stock.API.Models.Entities.Stock>();
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            List<bool> stockResult = new();
            foreach (OrderItemMessage orderItem in context.Message.OrderItems)
            {
                stockResult.Add((await _stockCollection.FindAsync(s => s.ProductId == orderItem.ProductId && s.Count >= orderItem.Count)).Any());
            }
            if (stockResult.TrueForAll(sr => sr.Equals(true)))
            {
                foreach (OrderItemMessage orderItem in context.Message.OrderItems)
                {
                    Stock.API.Models.Entities.Stock stock = await (await _stockCollection.FindAsync(s => s.ProductId == orderItem.ProductId))
                        .FirstOrDefaultAsync();
                    stock.Count -= orderItem.Count;
                    await _stockCollection.FindOneAndReplaceAsync(s => s.ProductId == orderItem.ProductId, stock);
                }

                //payment event yollayıp ödeme işlemlerini başlatma işlemleri
            }
            else
            {
                //siparişin tutarsız-geçersiz olduğuna dair işlemler
            }

        }
    }
}
