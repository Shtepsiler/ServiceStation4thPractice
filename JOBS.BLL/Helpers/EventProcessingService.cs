using JOBS.DAL.Data;
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
        private readonly EventListener<JobUpdatedEventDTO> _jobUpdateddListener;
        private readonly EventListener<JobPaidEventDTO> _jobPaidListener;
        public EventProcessingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            // Створюємо слухачі для подій OrderCreated та OrderPaid
            var configMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<Web3Config>>();
            _jobCreatedListener = new EventListener<JobCreatedEventDTO>(configMonitor);
            _jobUpdateddListener = new EventListener<JobUpdatedEventDTO>(configMonitor);
            _jobPaidListener = new EventListener<JobPaidEventDTO>(configMonitor);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🟢 Запуск сервісу слухання подій...");

            // Запускаємо слухачі подій
            _ = _jobCreatedListener.StartListeningAsync(HandleJobCreated, stoppingToken);
            _ = _jobUpdateddListener.StartListeningAsync(HandleJobUpdated, stoppingToken);
            _ = _jobPaidListener.StartListeningAsync(HandleJobPaid, stoppingToken);
        }

        private async void HandleJobCreated(JobCreatedEventDTO jobEvent)
        {
            Console.WriteLine($"📌 Нова подія JobCreated: JobId = {jobEvent.JobId}, CustomerId = {jobEvent.UserId}");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ServiceStationDBContext>();
                var job = dbContext.Jobs.FirstOrDefault(p => p.Id == Guid.Parse(jobEvent.JobId));
                if (job != null)
                {
                    job.jobIndex = (int)jobEvent.JobIndex;
                    
                }
                await dbContext.SaveChangesAsync();
                Console.WriteLine("✅ Роботу додано до бази даних!");
            }
        }

        private async void HandleJobUpdated(JobUpdatedEventDTO jobEvent)
        {
            Console.WriteLine($"📌 Нова подія JobCreated: JobId = {jobEvent.JobId}, CustomerId = {jobEvent.UserId}");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ServiceStationDBContext>();
                var job = dbContext.Jobs.FirstOrDefault(p => p.Id == Guid.Parse(jobEvent.JobId));
                if (job != null)
                {

                }
                //await dbContext.SaveChangesAsync();
                Console.WriteLine("✅ Роботу змінено!");
            }
        }
        private async void HandleJobPaid(JobPaidEventDTO jobEvent)
        {
            Console.WriteLine($"💰 Роботу {jobEvent.JobId} оплачено, : {jobEvent.Amount}");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ServiceStationDBContext>();

                var job = await dbContext.Jobs.FirstOrDefaultAsync(o => o.Id == Guid.Parse(jobEvent.JobId));
                if (job != null)
                {
                    job.IsPaid = true;
                    await dbContext.SaveChangesAsync();
                    Console.WriteLine("✅ Статус роботи оновлено у базі!");
                }
                else
                {
                    Console.WriteLine("⚠️ Роботу не знайдено!");
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
