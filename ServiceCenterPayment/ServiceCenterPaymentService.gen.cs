using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using ServiceCenterPayment.ContractDefinition;
using System.Numerics;

namespace ServiceCenterPayment
{
    public partial class ServiceCenterPaymentService : ServiceCenterPaymentServiceBase
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, ServiceCenterPaymentDeployment serviceCenterPaymentDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ServiceCenterPaymentDeployment>().SendRequestAndWaitForReceiptAsync(serviceCenterPaymentDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, ServiceCenterPaymentDeployment serviceCenterPaymentDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ServiceCenterPaymentDeployment>().SendRequestAsync(serviceCenterPaymentDeployment);
        }

        public static async Task<ServiceCenterPaymentService> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, ServiceCenterPaymentDeployment serviceCenterPaymentDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, serviceCenterPaymentDeployment, cancellationTokenSource);
            return new ServiceCenterPaymentService(web3, receipt.ContractAddress);
        }

        public ServiceCenterPaymentService(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

    }


    public partial class ServiceCenterPaymentServiceBase : ContractWeb3ServiceBase, IServiceCenterPaymentServiceBase
    {

        public ServiceCenterPaymentServiceBase(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

        public virtual Task<string> AddJobRequestAsync(AddJobFunction addJobFunction)
        {
            return ContractHandler.SendRequestAsync(addJobFunction);
        }

        public virtual Task<TransactionReceipt> AddJobRequestAndWaitForReceiptAsync(AddJobFunction addJobFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(addJobFunction, cancellationToken);
        }

        public virtual Task<string> AddJobRequestAsync(string userId, string jobId, BigInteger price)
        {
            var addJobFunction = new AddJobFunction();
            addJobFunction.UserId = userId;
            addJobFunction.JobId = jobId;
            addJobFunction.Price = price;

            return ContractHandler.SendRequestAsync(addJobFunction);
        }

        public virtual Task<TransactionReceipt> AddJobRequestAndWaitForReceiptAsync(string userId, string jobId, BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var addJobFunction = new AddJobFunction();
            addJobFunction.UserId = userId;
            addJobFunction.JobId = jobId;
            addJobFunction.Price = price;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(addJobFunction, cancellationToken);
        }

        public virtual Task<string> AddOrderRequestAsync(AddOrderFunction addOrderFunction)
        {
            return ContractHandler.SendRequestAsync(addOrderFunction);
        }

        public virtual Task<TransactionReceipt> AddOrderRequestAndWaitForReceiptAsync(AddOrderFunction addOrderFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(addOrderFunction, cancellationToken);
        }

        public virtual Task<string> AddOrderRequestAsync(string userId, string orderId, BigInteger price)
        {
            var addOrderFunction = new AddOrderFunction();
            addOrderFunction.UserId = userId;
            addOrderFunction.OrderId = orderId;
            addOrderFunction.Price = price;

            return ContractHandler.SendRequestAsync(addOrderFunction);
        }

        public virtual Task<TransactionReceipt> AddOrderRequestAndWaitForReceiptAsync(string userId, string orderId, BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var addOrderFunction = new AddOrderFunction();
            addOrderFunction.UserId = userId;
            addOrderFunction.OrderId = orderId;
            addOrderFunction.Price = price;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(addOrderFunction, cancellationToken);
        }

        public virtual Task<JobsOutputDTO> JobsQueryAsync(JobsFunction jobsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<JobsFunction, JobsOutputDTO>(jobsFunction, blockParameter);
        }

        public virtual Task<JobsOutputDTO> JobsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var jobsFunction = new JobsFunction();
            jobsFunction.ReturnValue1 = returnValue1;

            return ContractHandler.QueryDeserializingToObjectAsync<JobsFunction, JobsOutputDTO>(jobsFunction, blockParameter);
        }

        public virtual Task<OrdersOutputDTO> OrdersQueryAsync(OrdersFunction ordersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<OrdersFunction, OrdersOutputDTO>(ordersFunction, blockParameter);
        }

        public virtual Task<OrdersOutputDTO> OrdersQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var ordersFunction = new OrdersFunction();
            ordersFunction.ReturnValue1 = returnValue1;

            return ContractHandler.QueryDeserializingToObjectAsync<OrdersFunction, OrdersOutputDTO>(ordersFunction, blockParameter);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }


        public virtual Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public virtual Task<string> PayJobRequestAsync(PayJobFunction payJobFunction)
        {
            return ContractHandler.SendRequestAsync(payJobFunction);
        }

        public virtual Task<TransactionReceipt> PayJobRequestAndWaitForReceiptAsync(PayJobFunction payJobFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(payJobFunction, cancellationToken);
        }

        public virtual Task<string> PayJobRequestAsync(BigInteger jobIndex)
        {
            var payJobFunction = new PayJobFunction();
            payJobFunction.JobIndex = jobIndex;

            return ContractHandler.SendRequestAsync(payJobFunction);
        }

        public virtual Task<TransactionReceipt> PayJobRequestAndWaitForReceiptAsync(BigInteger jobIndex, CancellationTokenSource cancellationToken = null)
        {
            var payJobFunction = new PayJobFunction();
            payJobFunction.JobIndex = jobIndex;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(payJobFunction, cancellationToken);
        }

        public virtual Task<string> PayOrderRequestAsync(PayOrderFunction payOrderFunction)
        {
            return ContractHandler.SendRequestAsync(payOrderFunction);
        }

        public virtual Task<TransactionReceipt> PayOrderRequestAndWaitForReceiptAsync(PayOrderFunction payOrderFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(payOrderFunction, cancellationToken);
        }

        public virtual Task<string> PayOrderRequestAsync(BigInteger orderIndex)
        {
            var payOrderFunction = new PayOrderFunction();
            payOrderFunction.OrderIndex = orderIndex;

            return ContractHandler.SendRequestAsync(payOrderFunction);
        }

        public virtual Task<TransactionReceipt> PayOrderRequestAndWaitForReceiptAsync(BigInteger orderIndex, CancellationTokenSource cancellationToken = null)
        {
            var payOrderFunction = new PayOrderFunction();
            payOrderFunction.OrderIndex = orderIndex;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(payOrderFunction, cancellationToken);
        }

        public virtual Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction)
        {
            return ContractHandler.SendRequestAsync(renounceOwnershipFunction);
        }

        public virtual Task<string> RenounceOwnershipRequestAsync()
        {
            return ContractHandler.SendRequestAsync<RenounceOwnershipFunction>();
        }

        public virtual Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceOwnershipFunction, cancellationToken);
        }

        public virtual Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync<RenounceOwnershipFunction>(null, cancellationToken);
        }

        public virtual Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
            return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public virtual Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public virtual Task<string> TransferOwnershipRequestAsync(string newOwner)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
            transferOwnershipFunction.NewOwner = newOwner;

            return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public virtual Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
            transferOwnershipFunction.NewOwner = newOwner;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public virtual Task<string> UpdateJobRequestAsync(UpdateJobFunction updateJobFunction)
        {
            return ContractHandler.SendRequestAsync(updateJobFunction);
        }

        public virtual Task<TransactionReceipt> UpdateJobRequestAndWaitForReceiptAsync(UpdateJobFunction updateJobFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(updateJobFunction, cancellationToken);
        }

        public virtual Task<string> UpdateJobRequestAsync(BigInteger jobIndex, string userId, string jobId, BigInteger price)
        {
            var updateJobFunction = new UpdateJobFunction();
            updateJobFunction.JobIndex = jobIndex;
            updateJobFunction.UserId = userId;
            updateJobFunction.JobId = jobId;
            updateJobFunction.Price = price;

            return ContractHandler.SendRequestAsync(updateJobFunction);
        }

        public virtual Task<TransactionReceipt> UpdateJobRequestAndWaitForReceiptAsync(BigInteger jobIndex, string userId, string jobId, BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var updateJobFunction = new UpdateJobFunction();
            updateJobFunction.JobIndex = jobIndex;
            updateJobFunction.UserId = userId;
            updateJobFunction.JobId = jobId;
            updateJobFunction.Price = price;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(updateJobFunction, cancellationToken);
        }

        public virtual Task<string> UpdateOrderRequestAsync(UpdateOrderFunction updateOrderFunction)
        {
            return ContractHandler.SendRequestAsync(updateOrderFunction);
        }

        public virtual Task<TransactionReceipt> UpdateOrderRequestAndWaitForReceiptAsync(UpdateOrderFunction updateOrderFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(updateOrderFunction, cancellationToken);
        }

        public virtual Task<string> UpdateOrderRequestAsync(BigInteger orderIndex, string userId, string orderId, BigInteger price)
        {
            var updateOrderFunction = new UpdateOrderFunction();
            updateOrderFunction.OrderIndex = orderIndex;
            updateOrderFunction.UserId = userId;
            updateOrderFunction.OrderId = orderId;
            updateOrderFunction.Price = price;

            return ContractHandler.SendRequestAsync(updateOrderFunction);
        }

        public virtual Task<TransactionReceipt> UpdateOrderRequestAndWaitForReceiptAsync(BigInteger orderIndex, string userId, string orderId, BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var updateOrderFunction = new UpdateOrderFunction();
            updateOrderFunction.OrderIndex = orderIndex;
            updateOrderFunction.UserId = userId;
            updateOrderFunction.OrderId = orderId;
            updateOrderFunction.Price = price;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(updateOrderFunction, cancellationToken);
        }

        public virtual Task<string> WithdrawRequestAsync(WithdrawFunction withdrawFunction)
        {
            return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public virtual Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(WithdrawFunction withdrawFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public virtual Task<string> WithdrawRequestAsync(string recipient)
        {
            var withdrawFunction = new WithdrawFunction();
            withdrawFunction.Recipient = recipient;

            return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public virtual Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(string recipient, CancellationTokenSource cancellationToken = null)
        {
            var withdrawFunction = new WithdrawFunction();
            withdrawFunction.Recipient = recipient;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public override List<Type> GetAllFunctionTypes()
        {
            return new List<Type>
            {
                typeof(AddJobFunction),
                typeof(AddOrderFunction),
                typeof(JobsFunction),
                typeof(OrdersFunction),
                typeof(OwnerFunction),
                typeof(PayJobFunction),
                typeof(PayOrderFunction),
                typeof(RenounceOwnershipFunction),
                typeof(TransferOwnershipFunction),
                typeof(UpdateJobFunction),
                typeof(UpdateOrderFunction),
                typeof(WithdrawFunction)
            };
        }

        public override List<Type> GetAllEventTypes()
        {
            return new List<Type>
            {
                typeof(JobCreatedEventDTO),
                typeof(JobPaidEventDTO),
                typeof(JobUpdatedEventDTO),
                typeof(OrderCreatedEventDTO),
                typeof(OrderPaidEventDTO),
                typeof(OrderUpdatedEventDTO),
                typeof(OwnershipTransferredEventDTO)
            };
        }

        public override List<Type> GetAllErrorTypes()
        {
            return new List<Type>
            {
                typeof(OwnableInvalidOwnerError),
                typeof(OwnableUnauthorizedAccountError)
            };
        }
    }
}
