using Nethereum.RPC.Eth.DTOs;
using ServiceCenterPayment.ContractDefinition;
using System.Numerics;

namespace ServiceCenterPayment
{
    public interface IServiceCenterPaymentServiceBase
    {
        Task<TransactionReceipt> AddJobRequestAndWaitForReceiptAsync(AddJobFunction addJobFunction, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> AddJobRequestAndWaitForReceiptAsync(BigInteger jobIndex, string userId, string jobId, BigInteger price, CancellationTokenSource cancellationToken = null);
        Task<string> AddJobRequestAsync(AddJobFunction addJobFunction);
        Task<string> AddJobRequestAsync(BigInteger jobIndex, string userId, string jobId, BigInteger price);
        Task<TransactionReceipt> AddOrderRequestAndWaitForReceiptAsync(AddOrderFunction addOrderFunction, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> AddOrderRequestAndWaitForReceiptAsync(BigInteger orderIndex, string userId, string orderId, BigInteger price, CancellationTokenSource cancellationToken = null);
        Task<string> AddOrderRequestAsync(AddOrderFunction addOrderFunction);
        Task<string> AddOrderRequestAsync(BigInteger orderIndex, string userId, string orderId, BigInteger price);
        List<Type> GetAllErrorTypes();
        List<Type> GetAllEventTypes();
        List<Type> GetAllFunctionTypes();
        Task<JobsOutputDTO> JobsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null);
        Task<JobsOutputDTO> JobsQueryAsync(JobsFunction jobsFunction, BlockParameter blockParameter = null);
        Task<OrdersOutputDTO> OrdersQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null);
        Task<OrdersOutputDTO> OrdersQueryAsync(OrdersFunction ordersFunction, BlockParameter blockParameter = null);
        Task<string> OwnerQueryAsync(BlockParameter blockParameter = null);
        Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null);
        Task<TransactionReceipt> PayJobRequestAndWaitForReceiptAsync(BigInteger jobIndex, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> PayJobRequestAndWaitForReceiptAsync(PayJobFunction payJobFunction, CancellationTokenSource cancellationToken = null);
        Task<string> PayJobRequestAsync(BigInteger jobIndex);
        Task<string> PayJobRequestAsync(PayJobFunction payJobFunction);
        Task<TransactionReceipt> PayOrderRequestAndWaitForReceiptAsync(BigInteger orderIndex, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> PayOrderRequestAndWaitForReceiptAsync(PayOrderFunction payOrderFunction, CancellationTokenSource cancellationToken = null);
        Task<string> PayOrderRequestAsync(BigInteger orderIndex);
        Task<string> PayOrderRequestAsync(PayOrderFunction payOrderFunction);
        Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null);
        Task<string> RenounceOwnershipRequestAsync();
        Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction);
        Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null);
        Task<string> TransferOwnershipRequestAsync(string newOwner);
        Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction);
        Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(string recipient, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(WithdrawFunction withdrawFunction, CancellationTokenSource cancellationToken = null);
        Task<string> WithdrawRequestAsync(string recipient);
        Task<string> WithdrawRequestAsync(WithdrawFunction withdrawFunction);
    }
}