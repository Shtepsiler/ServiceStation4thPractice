using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PARTS.DAL.Data;
using PARTS.DAL.Entities;
using ServiceCenterPayment;
using ServiceCenterPayment.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.BLL.Services
{
    public class EventProcessingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EventListener<OrderCreatedEventDTO> _orderCreatedListener;
        private readonly EventListener<OrderPaidEventDTO> _orderPaidListener;
        public EventProcessingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
            // Дані для підключення до блокчейну
            string rpcUrl = Environment.GetEnvironmentVariable("infuraUrl"); 
            string contractAddress = Environment.GetEnvironmentVariable("deployedContractAddress"); 
            string privateKey = Environment.GetEnvironmentVariable("privateKey");
            var config = serviceProvider.GetService<Web3Config>();

            // Створюємо слухачі для подій OrderCreated та OrderPaid
            var configMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<Web3Config>>();

            _orderCreatedListener = new EventListener<OrderCreatedEventDTO>(configMonitor);
            _orderPaidListener = new EventListener<OrderPaidEventDTO>(configMonitor);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🟢 Запуск сервісу слухання подій...");

            // Запускаємо слухачі подій
            _ = _orderCreatedListener.StartListeningAsync(HandleOrderCreated, stoppingToken);
            _ = _orderPaidListener.StartListeningAsync(HandleOrderPaid, stoppingToken);
        }

        private async void HandleOrderCreated(OrderCreatedEventDTO orderEvent)
        {
            Console.WriteLine($"📌 Нова подія OrderCreated: OrderId = {orderEvent.OrderId}, CustomerId = {orderEvent.UserId}");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PartsDBContext>();
                var order = dbContext.Orders.FirstOrDefault(p => p.Id == Guid.Parse(orderEvent.OrderId));
                if(order!= null)
                {
                    order.Status = Status.Pending;
                }
                await dbContext.SaveChangesAsync();
                Console.WriteLine("✅ Замовлення додано до бази даних!");
            }
        }

        private async void HandleOrderPaid(OrderPaidEventDTO orderEvent)
        {
            Console.WriteLine($"💰 Замовлення {orderEvent.OrderId} оплачено, : {orderEvent.Amount}");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PartsDBContext>();

                var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == Guid.Parse(orderEvent.OrderId));
                if (order != null)
                {
                    order.IsPaid = true;
                    order.Status = Status.Paid;
                    await dbContext.SaveChangesAsync();
                    Console.WriteLine("✅ Статус замовлення оновлено у базі!");
                }
                else
                {
                    Console.WriteLine("⚠️ Замовлення не знайдено!");
                }
            }
        }
     
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("🔴 Зупинка сервісу слухання подій...");
            await base.StopAsync(cancellationToken);
        }
    }
}
