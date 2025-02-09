using Microsoft.Extensions.Options;
using Nethereum.Web3;
using ServiceCenterPayment.ContractDefinition;

namespace ServiceCenterPayment
{
    public interface IServiceCenterPaymentServiceFactory
    {
        Task<ServiceCenterPaymentService> CreateServiceAsync();
    }

    public class ServiceCenterPaymentServiceFactory : IServiceCenterPaymentServiceFactory
    {
        private readonly IWeb3 _web3;
        private readonly IOptionsMonitor<Web3Config> _settings;

        public ServiceCenterPaymentServiceFactory(IWeb3 web3, IOptionsMonitor<Web3Config> settings)
        {
            _web3 = web3;
            _settings = settings;
        }

        public async Task<ServiceCenterPaymentService> CreateServiceAsync()
        {
            var contractAddress = _settings.CurrentValue.ContractAddress;

            if (!string.IsNullOrEmpty(contractAddress))
            {
                bool isContractDeployed = await IsContractDeployedAsync(contractAddress);
                if (isContractDeployed)
                {
                    Console.WriteLine($"Contract already deployed at: {contractAddress}");
                    return new ServiceCenterPaymentService(_web3, contractAddress);
                }
            }

            return await DeployContractAndGetServiceAsync();
        }

        private async Task<bool> IsContractDeployedAsync(string contractAddress)
        {
            try
            {
                var code = await _web3.Eth.GetCode.SendRequestAsync(contractAddress);
                return !string.IsNullOrEmpty(code) && code != "0x";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking contract deployment: {ex.Message}");
                return false;
            }
        }

        private async Task<ServiceCenterPaymentService> DeployContractAndGetServiceAsync()
        {
            try
            {
                var service = await ServiceCenterPaymentService.DeployContractAndGetServiceAsync(_web3, new ServiceCenterPaymentDeployment());
                var contractAddress = service.ContractAddress;

                Console.WriteLine($"New contract deployed at: {contractAddress}");

                SaveContractAddress(contractAddress);

                return service;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deployment failed: {ex.Message}");
                throw;
            }
        }

        private void SaveContractAddress(string contractAddress)
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            if (!File.Exists(configPath))
            {
                Console.WriteLine("appsettings.json not found.");
                return;
            }

            try
            {
                var json = File.ReadAllText(configPath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                jsonObj["Blockchain"]["ContractAddress"] = contractAddress;

                var updatedJson = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(configPath, updatedJson);
                Console.WriteLine("Contract address saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving contract address: {ex.Message}");
            }
        }
    }
}
