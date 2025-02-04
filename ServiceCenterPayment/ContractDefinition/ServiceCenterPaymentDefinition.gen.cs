using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace ServiceCenterPayment.ContractDefinition
{


    public partial class ServiceCenterPaymentDeployment : ServiceCenterPaymentDeploymentBase
    {
        public ServiceCenterPaymentDeployment() : base(BYTECODE) { }
        public ServiceCenterPaymentDeployment(string byteCode) : base(byteCode) { }
    }

    public class ServiceCenterPaymentDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608080604052346071573315605e575f8054336001600160a01b0319821681178355916001600160a01b03909116907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e09080a3610bdb90816100768239f35b631e4fbdf760e01b5f525f60045260245ffd5b5f80fdfe60806040526004361015610011575f80fd5b5f3560e01c8063180aedf3146105d75780632a685d3c146104f857806351cff8d91461046c5780635a605b44146103ab578063715018a6146103545780638da5cb5b1461032d578063a85c38ef146102d7578063ad966cb71461020c578063f2fde38b146101835763f78fe41a14610087575f80fd5b602036600319011261017f57600435805f52600160205260405f20600281015490811561014557600381019081549160ff831661010d576001610108936100f07f5a24bde1cdbc136f9b10186921e55762d88e2920ac6f114769b8103056c03aac963414610830565b60ff191617905560405191829134906001018361087c565b0390a2005b60405162461bcd60e51b815260206004820152601060248201526f129bd888185b1c9958591e481c185a5960821b6044820152606490fd5b60405162461bcd60e51b8152602060048201526012602482015271129bd888191bd95cc81b9bdd08195e1a5cdd60721b6044820152606490fd5b5f80fd5b3461017f57602036600319011261017f576004356001600160a01b0381169081900361017f576101b1610b7f565b80156101f9575f80546001600160a01b03198116831782556001600160a01b0316907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e09080a3005b631e4fbdf760e01b5f525f60045260245ffd5b3461017f5761021a366107da565b91610223610b7f565b835f526001602052600260405f20015461029d576101087fc5e57fec369f75d1dc51881cfb8dab9bafc5b45d7b1bf0f74aba215878fb9efa9361029160405161026b81610637565b8481528560208201528260408201525f6060820152875f52600160205260405f20610910565b60405193849384610b51565b60405162461bcd60e51b81526020600482015260126024820152714a6f6220616c72656164792065786973747360701b6044820152606490fd5b3461017f57602036600319011261017f576004355f52600260205260405f206102ff81610689565b61032961030e60018401610689565b9260ff6003600283015492015416906040519485948561074d565b0390f35b3461017f575f36600319011261017f575f546040516001600160a01b039091168152602090f35b3461017f575f36600319011261017f5761036c610b7f565b5f80546001600160a01b0319811682556001600160a01b03167f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e08280a3005b3461017f576103b9366107da565b916103c2610b7f565b835f526002602052600260405f200154610430576101087fbe12ed8f93c2b758a2d950e8ff373dcc019923a1166727388c609e7a3b514f5a9361029160405161040a81610637565b8481528560208201528260408201525f6060820152875f52600260205260405f20610910565b60405162461bcd60e51b81526020600482015260146024820152734f7264657220616c72656164792065786973747360601b6044820152606490fd5b3461017f57602036600319011261017f576004356001600160a01b0381169081900361017f5761049a610b7f565b479081156104bc575f808093819382f1156104b157005b6040513d5f823e3d90fd5b60405162461bcd60e51b81526020600482015260146024820152734e6f2066756e647320746f20776974686472617760601b6044820152606490fd5b602036600319011261017f57600435805f52600260205260405f20600281015490811561059b57600381019081549160ff8316610561576001610108936100f07ff2380ef6bb1ffba9de09f771413f94e5e72320b021db566192b7b7958751df98963414610830565b60405162461bcd60e51b815260206004820152601260248201527113dc99195c88185b1c9958591e481c185a5960721b6044820152606490fd5b60405162461bcd60e51b815260206004820152601460248201527313dc99195c88191bd95cc81b9bdd08195e1a5cdd60621b6044820152606490fd5b3461017f57602036600319011261017f576004355f52600160205260405f206102ff81610689565b90600182811c9216801561062d575b602083101461061957565b634e487b7160e01b5f52602260045260245ffd5b91607f169161060e565b6080810190811067ffffffffffffffff82111761065357604052565b634e487b7160e01b5f52604160045260245ffd5b90601f8019910116810190811067ffffffffffffffff82111761065357604052565b9060405191825f82549261069c846105ff565b808452936001811690811561070757506001146106c3575b506106c192500383610667565b565b90505f9291925260205f20905f915b8183106106eb5750509060206106c1928201015f6106b4565b60209193508060019154838589010152019101909184926106d2565b9050602092506106c194915060ff191682840152151560051b8201015f6106b4565b805180835260209291819084018484015e5f828201840152601f01601f1916010190565b9294939060609261076961077792608087526080870190610729565b908582036020870152610729565b9460408401521515910152565b81601f8201121561017f5780359067ffffffffffffffff821161065357604051926107b9601f8401601f191660200185610667565b8284526020838301011161017f57815f926020809301838601378301015290565b608060031982011261017f576004359160243567ffffffffffffffff811161017f578261080991600401610784565b916044359067ffffffffffffffff821161017f5761082991600401610784565b9060643590565b1561083757565b60405162461bcd60e51b815260206004820152601860248201527f496e636f7272656374207061796d656e7420616d6f756e7400000000000000006044820152606490fd5b929190604084525f815491610890836105ff565b928360408801526001811690815f146108ef57506001146108b6575b5060209150930152565b90505f5260205f205f905b8282106108d857506020915084016060015f6108ac565b600181602092546060858a010152019101906108c1565b905060609250602093915060ff191682870152151560051b8501015f6108ac565b9190805192835167ffffffffffffffff81116106535761093082546105ff565b601f8111610b0c575b50602094601f8211600114610aab579481929394955f92610aa0575b50508160011b915f199060031b1c19161781555b60018101602083015180519067ffffffffffffffff82116106535761098e83546105ff565b601f8111610a5b575b50602090601f83116001146109f357918060039492606096945f926109e8575b50508160011b915f1990861b1c19161790555b6040840151600282015501910151151560ff80198354169116179055565b015190505f806109b7565b90601f19831691845f52815f20925f5b818110610a435750926001928592606098966003989610610a2c575b505050811b0190556109ca565b01515f1983881b60f8161c191690555f8080610a1f565b92936020600181928786015181550195019301610a03565b835f5260205f20601f840160051c81019160208510610a96575b601f0160051c01905b818110610a8b5750610997565b5f8155600101610a7e565b9091508190610a75565b015190505f80610955565b601f19821695835f52805f20915f5b888110610af457508360019596979810610adc575b505050811b018155610969565b01515f1960f88460031b161c191690555f8080610acf565b91926020600181928685015181550194019201610aba565b825f5260205f20601f830160051c81019160208410610b47575b601f0160051c01905b818110610b3c5750610939565b5f8155600101610b2f565b9091508190610b26565b939291610b7a90610b6c604093606088526060880190610729565b908682036020880152610729565b930152565b5f546001600160a01b03163303610b9257565b63118cdaa760e01b5f523360045260245ffdfea264697066735822122086da8cbfeee12c066b20254c4bc7815a3c1ab591dc2df191700e8908b807486064736f6c634300081c0033";
        public ServiceCenterPaymentDeploymentBase() : base(BYTECODE) { }
        public ServiceCenterPaymentDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AddJobFunction : AddJobFunctionBase { }

    [Function("addJob")]
    public class AddJobFunctionBase : FunctionMessage
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

    public partial class AddOrderFunction : AddOrderFunctionBase { }

    [Function("addOrder")]
    public class AddOrderFunctionBase : FunctionMessage
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
