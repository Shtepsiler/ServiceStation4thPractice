using Microsoft.Extensions.Options;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceCenterPayment
{
    public class EventListener<T> where T : IEventDTO, new()
    {
        private readonly Web3 _web3;
        private readonly string _contractAddress;
        private readonly Event<T> _event;

        public EventListener(IOptionsMonitor<Web3Config> config)
        {
            var account = new Account(config.CurrentValue.PrivateKey);
            _web3 = new Web3(account, config.CurrentValue.RpcUrl);
            _contractAddress = config.CurrentValue.ContractAddress;
            _event = _web3.Eth.GetEvent<T>(_contractAddress);
        }

        public async Task StartListeningAsync(Action<T> handleEvent, CancellationToken cancellationToken)
        {
            var filterId = await _event.CreateFilterAsync();

            await Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var logs = await _event.GetFilterChangesAsync(filterId);
                        foreach (var log in logs)
                        {
                            handleEvent?.Invoke(log.Event); // Викликаємо передану функцію
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка під час обробки події: {ex.Message}");
                        throw ex;
                    }
                    await Task.Delay(2000, cancellationToken);
                }
            }, cancellationToken);
        }

        public async Task<List<T>> GetPastEventsAsync()
        {
            var filterInput = _event.CreateFilterInput();
            var logs = await _event.GetAllChangesAsync(filterInput);

            var eventList = new List<T>();
            foreach (var log in logs)
            {
                eventList.Add(log.Event);
            }
            return eventList;
        }
    }
}
