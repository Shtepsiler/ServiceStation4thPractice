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
            if (string.IsNullOrEmpty(_settings.CurrentValue.ContractAddress))
            {
                return await DeployContractAndGetServiceAsync();
            }
            Console.WriteLine(_settings.CurrentValue.ContractAddress);

            return new ServiceCenterPaymentService(_web3, _settings.CurrentValue.ContractAddress);
        }

        private async Task<ServiceCenterPaymentService> DeployContractAndGetServiceAsync()
        {
            var service = await ServiceCenterPaymentService.DeployContractAndGetServiceAsync(_web3, new ServiceCenterPaymentDeployment());
            var contractAddress = service.ContractAddress;

            _settings.CurrentValue.ContractAddress = contractAddress;
            Console.WriteLine(contractAddress);

            SaveContractAddress(contractAddress);

            return service;
        }

        private void SaveContractAddress(string contractAddress)
        {


            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var json = File.ReadAllText(configPath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj["Blockchain"]["ContractAddress"] = contractAddress;

            var updatedJson = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(configPath, updatedJson);
        }
    }
}
