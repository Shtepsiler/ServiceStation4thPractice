using Microsoft.Extensions.Options;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace ServiceCenterPayment
{
    public class Web3Config
    {
        public string RpcUrl { get; set; }
        public string PrivateKey { get; set; }
        public string ContractAddress { get; set; }
        public string RpcLocalUrl { get; set; }
    }

    public class Web3Context
    {
        public IWeb3 Web3 { get; }
        public Account Account { get; }
        public string ContractAddress { get; }

        public Web3Context(IOptionsMonitor<Web3Config> config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            var configuration = config.CurrentValue;

            // Визначаємо який RPC URL використовувати залежно від середовища
            string rpcUrl = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
                ? configuration.RpcUrl
                : configuration.RpcLocalUrl;

            if (string.IsNullOrEmpty(rpcUrl))
            {
                throw new ArgumentException("No RPC URL configuration specified for current environment.");
            }

            Account = new Account(configuration.PrivateKey);
            Web3 = new Web3(Account, rpcUrl);
            ContractAddress = configuration.ContractAddress;
        }
    }
}