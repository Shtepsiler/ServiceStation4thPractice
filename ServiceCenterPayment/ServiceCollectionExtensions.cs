using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace ServiceCenterPayment
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeb3Context(this IServiceCollection services)
         {
            // Реєструємо Web3Context як Singleton
            services.AddSingleton<Web3Context>();

            // Реєструємо IWeb3 для взаємодії з блокчейном
            services.AddSingleton<IWeb3>(provider =>
            {
                var config = provider.GetRequiredService<IOptions<Web3Config>>().Value;
                var account = new Account(config.PrivateKey);
                return new Web3(account, config.RpcUrl);
            });

            // Додаємо фабрику для створення ServiceCenterPaymentService
            services.AddSingleton<IServiceCenterPaymentServiceFactory, ServiceCenterPaymentServiceFactory>();

            return services;
        }
    }
}
