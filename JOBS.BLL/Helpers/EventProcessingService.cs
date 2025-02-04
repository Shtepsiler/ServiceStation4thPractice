﻿using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ServiceCenterPayment;
using ServiceCenterPayment.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOBS.BLL.Helpers
{
    public class EventProcessingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EventListener<JobCreatedEventDTO> _jobCreatedListener;
        private readonly EventListener<JobPaidEventDTO> _jobPaidListener;
        public EventProcessingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            // Дані для підключення до блокчейну
            string rpcUrl = Environment.GetEnvironmentVariable("infuraUrl"); ;
            string contractAddress = Environment.GetEnvironmentVariable("deployedContractAddress"); ;
            string privateKey = Environment.GetEnvironmentVariable("privateKey"); ;

            // Створюємо слухачі для подій OrderCreated та OrderPaid
            var configMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<Web3Config>>();
            _jobCreatedListener = new EventListener<JobCreatedEventDTO>(configMonitor);
            _jobPaidListener = new EventListener<JobPaidEventDTO>(configMonitor);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🟢 Запуск сервісу слухання подій...");

            // Запускаємо слухачі подій
            _ = _jobCreatedListener.StartListeningAsync(HandleJobCreated, stoppingToken);
            _ = _jobPaidListener.StartListeningAsync(HandleJobPaid, stoppingToken);
        }

        private async void HandleJobCreated(JobCreatedEventDTO orderEvent)
        {
            Console.WriteLine($"📌 Нова подія JobCreated: JobId = {orderEvent.JobId}, CustomerId = {orderEvent.UserId}");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ServiceStationDBContext>();
                var order = dbContext.Jobs.FirstOrDefault(p => p.Id == Guid.Parse(orderEvent.JobId));
                if (order != null)
                {
                   
                }
                await dbContext.SaveChangesAsync();
                Console.WriteLine("✅ Замовлення додано до бази даних!");
            }
        }

        private async void HandleJobPaid(JobPaidEventDTO orderEvent)
        {
            Console.WriteLine($"💰 Замовлення {orderEvent.JobId} оплачено, : {orderEvent.Amount}");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ServiceStationDBContext>();

                var order = await dbContext.Jobs.FirstOrDefaultAsync(o => o.Id == Guid.Parse(orderEvent.JobId));
                if (order != null)
                {
                    order.IsPaid = true;
                    order.Status = Status.Completed;
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
