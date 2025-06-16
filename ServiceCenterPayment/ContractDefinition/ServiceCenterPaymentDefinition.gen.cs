using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace ServiceCenterPayment.ContractDefinition
{


    public partial class ServiceCenterPaymentDeployment : ServiceCenterPaymentDeploymentBase
    {
        public ServiceCenterPaymentDeployment() : base(BYTECODE) { }
        public ServiceCenterPaymentDeployment(string byteCode) : base(byteCode) { }
    }

    public class ServiceCenterPaymentDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608080604052346071573315605e575f8054336001600160a01b0319821681178355916001600160a01b03909116907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e09080a3610fa490816100768239f35b631e4fbdf760e01b5f525f60045260245ffd5b5f80fdfe60806040526004361015610011575f80fd5b5f3560e01c8063180aedf3146108785780632a685d3c146107cf5780633d1f145d1461064757806351cff8d9146105bb578063715018a61461056457806383234e6b146104e35780638a21b253146102ff5780638da5cb5b146102d8578063a85c38ef14610282578063f2fde38b146101f9578063f78fe41a146101335763fa6b104a1461009d575f80fd5b3461012f577fc5e57fec369f75d1dc51881cfb8dab9bafc5b45d7b1bf0f74aba215878fb9efa6100cc36610abd565b92916100d6610f48565b61012a600154946100e686610ce5565b60015561011e6040516100f8816108d8565b8481528560208201528260408201525f6060820152875f52600360205260405f20610d07565b60405193849384610cbc565b0390a2005b5f80fd5b602036600319011261012f57600435805f52600360205260405f2061016261015b82546108a0565b1515610c3c565b600381019081549060ff82166101c1577f5a24bde1cdbc136f9b10186921e55762d88e2920ac6f114769b8103056c03aac92600161012a936101a960028501543414610b53565b60ff1916179055604051918291349060010183610c20565b60405162461bcd60e51b815260206004820152601060248201526f129bd888185b1c9958591e481c185a5960821b6044820152606490fd5b3461012f57602036600319011261012f576004356001600160a01b0381169081900361012f57610227610f48565b801561026f575f80546001600160a01b03198116831782556001600160a01b0316907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e09080a3005b631e4fbdf760e01b5f525f60045260245ffd5b3461012f57602036600319011261012f576004355f52600460205260405f206102aa81610916565b6102d46102b960018401610916565b9260ff600360028301549201541690604051948594856109da565b0390f35b3461012f575f36600319011261012f575f546040516001600160a01b039091168152602090f35b3461012f5761030d36610a67565b9291610317610f48565b825f52600460205260405f209361033861033186546108a0565b1515610b10565b6103676040516103538161034c818a610b9f565b03826108f4565b602081519101208351602085012014610c7d565b8251946001810167ffffffffffffffff87116104cf5761038781546108a0565b601f811161048a575b50602096601f811160011461040057839261012a9492826002937fe36ab76905e432e43816cd57477913bf54039d7bb26ae017578c8645e0f5fd0b9a9b5f916103f5575b508160011b915f199060031b1c19161790555b015560405193849384610cbc565b90508901518c6103d4565b601f198116825f52885f20905f5b8181106104725750926001838796937fe36ab76905e432e43816cd57477913bf54039d7bb26ae017578c8645e0f5fd0b9b9c60029661012a9a981061045a575b5050811b0190556103e7565b8b01515f1960f88460031b161c191690558c8061044e565b888b0151835560209a8b019a6001909301920161040e565b815f5260205f20601f890160051c81019160208a106104c5575b601f0160051c01905b8181106104ba5750610390565b5f81556001016104ad565b90915081906104a4565b634e487b7160e01b5f52604160045260245ffd5b3461012f577fbe12ed8f93c2b758a2d950e8ff373dcc019923a1166727388c609e7a3b514f5a61051236610abd565b929161051c610f48565b61012a6002549461052c86610ce5565b60025561011e60405161053e816108d8565b8481528560208201528260408201525f6060820152875f52600460205260405f20610d07565b3461012f575f36600319011261012f5761057c610f48565b5f80546001600160a01b0319811682556001600160a01b03167f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e08280a3005b3461012f57602036600319011261012f576004356001600160a01b0381169081900361012f576105e9610f48565b4790811561060b575f808093819382f11561060057005b6040513d5f823e3d90fd5b60405162461bcd60e51b81526020600482015260146024820152734e6f2066756e647320746f20776974686472617760601b6044820152606490fd5b3461012f5761065536610a67565b929161065f610f48565b825f52600360205260405f209361067961015b86546108a0565b61068d6040516103538161034c818a610b9f565b8251946001810167ffffffffffffffff87116104cf576106ad81546108a0565b601f811161078a575b50602096601f811160011461071957839261012a9492826002937f45443df918230427dc362cef1053a9b732753626d624a9b057e22f4130682d999a9b5f916103f557508160011b915f199060031b1c1916179055015560405193849384610cbc565b601f198116825f52885f20905f5b8181106107725750926001838796937f45443df918230427dc362cef1053a9b732753626d624a9b057e22f4130682d999b9c60029661012a9a981061045a575050811b0190556103e7565b888b0151835560209a8b019a60019093019201610727565b815f5260205f20601f890160051c81019160208a106107c5575b601f0160051c01905b8181106107ba57506106b6565b5f81556001016107ad565b90915081906107a4565b602036600319011261012f57600435805f52600460205260405f206107f761033182546108a0565b600381019081549060ff821661083e577ff2380ef6bb1ffba9de09f771413f94e5e72320b021db566192b7b7958751df9892600161012a936101a960028501543414610b53565b60405162461bcd60e51b815260206004820152601260248201527113dc99195c88185b1c9958591e481c185a5960721b6044820152606490fd5b3461012f57602036600319011261012f576004355f52600360205260405f206102aa81610916565b90600182811c921680156108ce575b60208310146108ba57565b634e487b7160e01b5f52602260045260245ffd5b91607f16916108af565b6080810190811067ffffffffffffffff8211176104cf57604052565b90601f8019910116810190811067ffffffffffffffff8211176104cf57604052565b9060405191825f825492610929846108a0565b80845293600181169081156109945750600114610950575b5061094e925003836108f4565b565b90505f9291925260205f20905f915b81831061097857505090602061094e928201015f610941565b602091935080600191548385890101520191019091849261095f565b90506020925061094e94915060ff191682840152151560051b8201015f610941565b805180835260209291819084018484015e5f828201840152601f01601f1916010190565b929493906060926109f6610a04926080875260808701906109b6565b9085820360208701526109b6565b9460408401521515910152565b81601f8201121561012f5780359067ffffffffffffffff82116104cf5760405192610a46601f8401601f1916602001856108f4565b8284526020838301011161012f57815f926020809301838601378301015290565b608060031982011261012f576004359160243567ffffffffffffffff811161012f5782610a9691600401610a11565b916044359067ffffffffffffffff821161012f57610ab691600401610a11565b9060643590565b90606060031983011261012f5760043567ffffffffffffffff811161012f5782610ae991600401610a11565b916024359067ffffffffffffffff821161012f57610b0991600401610a11565b9060443590565b15610b1757565b60405162461bcd60e51b815260206004820152601460248201527313dc99195c88191bd95cc81b9bdd08195e1a5cdd60621b6044820152606490fd5b15610b5a57565b60405162461bcd60e51b815260206004820152601860248201527f496e636f7272656374207061796d656e7420616d6f756e7400000000000000006044820152606490fd5b5f9291815491610bae836108a0565b8083529260018116908115610c035750600114610bca57505050565b5f9081526020812093945091925b838310610be9575060209250010190565b600181602092949394548385870101520191019190610bd8565b915050602093945060ff929192191683830152151560051b010190565b929190610c37602091604086526040860190610b9f565b930152565b15610c4357565b60405162461bcd60e51b8152602060048201526012602482015271129bd888191bd95cc81b9bdd08195e1a5cdd60721b6044820152606490fd5b15610c8457565b60405162461bcd60e51b815260206004820152601060248201526f0aae6cae440928840dad2e6dac2e8c6d60831b6044820152606490fd5b939291610c3790610cd76040936060885260608801906109b6565b9086820360208801526109b6565b5f198114610cf35760010190565b634e487b7160e01b5f52601160045260245ffd5b9190805192835167ffffffffffffffff81116104cf57610d2782546108a0565b601f8111610f03575b50602094601f8211600114610ea2579481929394955f92610e97575b50508160011b915f199060031b1c19161781555b60018101602083015180519067ffffffffffffffff82116104cf57610d8583546108a0565b601f8111610e52575b50602090601f8311600114610dea57918060039492606096945f92610ddf575b50508160011b915f1990861b1c19161790555b6040840151600282015501910151151560ff80198354169116179055565b015190505f80610dae565b90601f19831691845f52815f20925f5b818110610e3a5750926001928592606098966003989610610e23575b505050811b019055610dc1565b01515f1983881b60f8161c191690555f8080610e16565b92936020600181928786015181550195019301610dfa565b835f5260205f20601f840160051c81019160208510610e8d575b601f0160051c01905b818110610e825750610d8e565b5f8155600101610e75565b9091508190610e6c565b015190505f80610d4c565b601f19821695835f52805f20915f5b888110610eeb57508360019596979810610ed3575b505050811b018155610d60565b01515f1960f88460031b161c191690555f8080610ec6565b91926020600181928685015181550194019201610eb1565b825f5260205f20601f830160051c81019160208410610f3e575b601f0160051c01905b818110610f335750610d30565b5f8155600101610f26565b9091508190610f1d565b5f546001600160a01b03163303610f5b57565b63118cdaa760e01b5f523360045260245ffdfea2646970667358221220306118f742040790919a43f58d32d740e2692cab15a0964344219bb6defa73a164736f6c634300081c0033";
        public ServiceCenterPaymentDeploymentBase() : base(BYTECODE) { }
        public ServiceCenterPaymentDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AddJobFunction : AddJobFunctionBase { }

    [Function("addJob")]
    public class AddJobFunctionBase : FunctionMessage
    {
        [Parameter("string", "userId", 1)]
        public virtual string UserId { get; set; }
        [Parameter("string", "jobId", 2)]
        public virtual string JobId { get; set; }
        [Parameter("uint256", "price", 3)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class AddOrderFunction : AddOrderFunctionBase { }

    [Function("addOrder")]
    public class AddOrderFunctionBase : FunctionMessage
    {
        [Parameter("string", "userId", 1)]
        public virtual string UserId { get; set; }
        [Parameter("string", "orderId", 2)]
        public virtual string OrderId { get; set; }
        [Parameter("uint256", "price", 3)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class JobsFunction : JobsFunctionBase { }

    [Function("jobs", typeof(JobsOutputDTO))]
    public class JobsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OrdersFunction : OrdersFunctionBase { }

    [Function("orders", typeof(OrdersOutputDTO))]
    public class OrdersFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class PayJobFunction : PayJobFunctionBase { }

    [Function("payJob")]
    public class PayJobFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "jobIndex", 1)]
        public virtual BigInteger JobIndex { get; set; }
    }

    public partial class PayOrderFunction : PayOrderFunctionBase { }

    [Function("payOrder")]
    public class PayOrderFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "orderIndex", 1)]
        public virtual BigInteger OrderIndex { get; set; }
    }

    public partial class RenounceOwnershipFunction : RenounceOwnershipFunctionBase { }

    [Function("renounceOwnership")]
    public class RenounceOwnershipFunctionBase : FunctionMessage
    {

    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class UpdateJobFunction : UpdateJobFunctionBase { }

    [Function("updateJob")]
    public class UpdateJobFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "jobIndex", 1)]
        public virtual BigInteger JobIndex { get; set; }
        [Parameter("string", "userId", 2)]
        public virtual string UserId { get; set; }
        [Parameter("string", "jobId", 3)]
        public virtual string JobId { get; set; }
        [Parameter("uint256", "price", 4)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class UpdateOrderFunction : UpdateOrderFunctionBase { }

    [Function("updateOrder")]
    public class UpdateOrderFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "orderIndex", 1)]
        public virtual BigInteger OrderIndex { get; set; }
        [Parameter("string", "userId", 2)]
        public virtual string UserId { get; set; }
        [Parameter("string", "orderId", 3)]
        public virtual string OrderId { get; set; }
        [Parameter("uint256", "price", 4)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class WithdrawFunction : WithdrawFunctionBase { }

    [Function("withdraw")]
    public class WithdrawFunctionBase : FunctionMessage
    {
        [Parameter("address", "recipient", 1)]
        public virtual string Recipient { get; set; }
    }

    public partial class JobCreatedEventDTO : JobCreatedEventDTOBase { }

    [Event("JobCreated")]
    public class JobCreatedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "jobIndex", 1, true)]
        public virtual BigInteger JobIndex { get; set; }
        [Parameter("string", "userId", 2, false)]
        public virtual string UserId { get; set; }
        [Parameter("string", "jobId", 3, false)]
        public virtual string JobId { get; set; }
        [Parameter("uint256", "price", 4, false)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class JobPaidEventDTO : JobPaidEventDTOBase { }

    [Event("JobPaid")]
    public class JobPaidEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "jobIndex", 1, true)]
        public virtual BigInteger JobIndex { get; set; }
        [Parameter("string", "jobId", 2, false)]
        public virtual string JobId { get; set; }
        [Parameter("uint256", "amount", 3, false)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class JobUpdatedEventDTO : JobUpdatedEventDTOBase { }

    [Event("JobUpdated")]
    public class JobUpdatedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "jobIndex", 1, true)]
        public virtual BigInteger JobIndex { get; set; }
        [Parameter("string", "userId", 2, false)]
        public virtual string UserId { get; set; }
        [Parameter("string", "jobId", 3, false)]
        public virtual string JobId { get; set; }
        [Parameter("uint256", "price", 4, false)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class OrderCreatedEventDTO : OrderCreatedEventDTOBase { }

    [Event("OrderCreated")]
    public class OrderCreatedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "orderIndex", 1, true)]
        public virtual BigInteger OrderIndex { get; set; }
        [Parameter("string", "userId", 2, false)]
        public virtual string UserId { get; set; }
        [Parameter("string", "orderId", 3, false)]
        public virtual string OrderId { get; set; }
        [Parameter("uint256", "price", 4, false)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class OrderPaidEventDTO : OrderPaidEventDTOBase { }

    [Event("OrderPaid")]
    public class OrderPaidEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "orderIndex", 1, true)]
        public virtual BigInteger OrderIndex { get; set; }
        [Parameter("string", "orderId", 2, false)]
        public virtual string OrderId { get; set; }
        [Parameter("uint256", "amount", 3, false)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class OrderUpdatedEventDTO : OrderUpdatedEventDTOBase { }

    [Event("OrderUpdated")]
    public class OrderUpdatedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "orderIndex", 1, true)]
        public virtual BigInteger OrderIndex { get; set; }
        [Parameter("string", "userId", 2, false)]
        public virtual string UserId { get; set; }
        [Parameter("string", "orderId", 3, false)]
        public virtual string OrderId { get; set; }
        [Parameter("uint256", "price", 4, false)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "previousOwner", 1, true)]
        public virtual string PreviousOwner { get; set; }
        [Parameter("address", "newOwner", 2, true)]
        public virtual string NewOwner { get; set; }
    }

    public partial class OwnableInvalidOwnerError : OwnableInvalidOwnerErrorBase { }

    [Error("OwnableInvalidOwner")]
    public class OwnableInvalidOwnerErrorBase : IErrorDTO
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
    }

    public partial class OwnableUnauthorizedAccountError : OwnableUnauthorizedAccountErrorBase { }

    [Error("OwnableUnauthorizedAccount")]
    public class OwnableUnauthorizedAccountErrorBase : IErrorDTO
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
    }





    public partial class JobsOutputDTO : JobsOutputDTOBase { }

    [FunctionOutput]
    public class JobsOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string", "userId", 1)]
        public virtual string UserId { get; set; }
        [Parameter("string", "jobId", 2)]
        public virtual string JobId { get; set; }
        [Parameter("uint256", "price", 3)]
        public virtual BigInteger Price { get; set; }
        [Parameter("bool", "isPaid", 4)]
        public virtual bool IsPaid { get; set; }
    }

    public partial class OrdersOutputDTO : OrdersOutputDTOBase { }

    [FunctionOutput]
    public class OrdersOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string", "userId", 1)]
        public virtual string UserId { get; set; }
        [Parameter("string", "orderId", 2)]
        public virtual string OrderId { get; set; }
        [Parameter("uint256", "price", 3)]
        public virtual BigInteger Price { get; set; }
        [Parameter("bool", "isPaid", 4)]
        public virtual bool IsPaid { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }














}
