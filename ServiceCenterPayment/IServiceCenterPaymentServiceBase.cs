using Nethereum.RPC.Eth.DTOs;
using ServiceCenterPayment.ContractDefinition;
using System.Numerics;

namespace ServiceCenterPayment
{
    public interface IServiceCenterPaymentServiceBase
    {
        Task<JobsOutputDTO> JobsQueryAsync(JobsFunction jobsFunction, BlockParameter blockParameter = null);
        Task<OrdersOutputDTO> OrdersQueryAsync(OrdersFunction ordersFunction, BlockParameter blockParameter = null);
        Task<string> UpdateJobRequestAsync(UpdateJobFunction updateJobFunction);
        Task<string> UpdateOrderRequestAsync(UpdateOrderFunction updateOrderFunction);


        Task<TransactionReceipt> AddJobRequestAndWaitForReceiptAsync(AddJobFunction addJobFunction, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> UpdateJobRequestAndWaitForReceiptAsync(UpdateJobFunction updateJobFunction, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> PayJobRequestAndWaitForReceiptAsync(PayJobFunction payJobFunction, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> AddOrderRequestAndWaitForReceiptAsync(AddOrderFunction addOrderFunction, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> UpdateOrderRequestAndWaitForReceiptAsync(UpdateOrderFunction updateOrderFunction, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> PayOrderRequestAndWaitForReceiptAsync(PayOrderFunction payOrderFunction, CancellationTokenSource cancellationToken = null);
    }
}