using System;
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
    }

    public class Web3Context
    {
        public IWeb3 Web3 { get; }
        public Account Account { get; }
        public string ContractAddress { get; }

        public Web3Context(IOptionsMonitor<Web3Config> config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            Account = new Account(config.CurrentValue.PrivateKey);
            Web3 = new Web3(Account, config.CurrentValue.RpcUrl);
            ContractAddress = config.CurrentValue.ContractAddress;
        }
    }
}
