using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Entities;
using PARTS.DAL.Interfaces;
using ServiceCenterPayment;
using System.Numerics;

namespace PARTS.BLL.Services
{
    public class OrderService : GenericService<Order, OrderRequest, OrderResponse>, IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IServiceCenterPaymentServiceFactory serviceCenterPaymentFactory;

        public OrderService(IOrderRepository repository, IMapper mapper, IServiceCenterPaymentServiceFactory serviceCenterPayment)
            : base(repository, mapper)
        {
            this.repository = repository;
            serviceCenterPaymentFactory = serviceCenterPayment;
            OnPriceUpdated += ChangePrice;
        }
        // Делегат для оновлення ціни
        public delegate Task PriceUpdatedHandler(Guid orderId);

        // Подія, що викликається при зміні ціни
        public event PriceUpdatedHandler? OnPriceUpdated;
        public override async Task<OrderResponse> PostAsync(OrderRequest rec)
        {
            try
            {
                rec.WEIPrice = EthereumPriceConverter.ConvertUsdToEtherAsync(0, 18).Result.ToString();
                var order = await base.PostAsync(rec);

                var ser = await serviceCenterPaymentFactory.CreateServiceAsync();
                var tf = await ser.AddOrderRequestAndWaitForReceiptAsync(new()
                {
                    UserId = rec.СustomerId.ToString(),
                    OrderId = order.Id.ToString(),
                    Price = BigInteger.Parse(order.WEIPrice)
                });
                return order;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task AddPartToOrderAsync(Guid orderId, Guid partId)
        {
            await repository.AddPartToOrderAsync(orderId, partId);

            // Викликаємо подію після додавання частини
            if (OnPriceUpdated != null)
            {
                await OnPriceUpdated(orderId);
            }

        }

        public async Task RemovePartFromOrderAsync(Guid orderId, Guid partId)
        {
            await repository.RemovePartFromOrderAsync(orderId, partId);

            // Викликаємо подію після додавання частини
            if (OnPriceUpdated != null)
            {
                await OnPriceUpdated(orderId);
            }
        }

        private async Task ChangePrice(Guid orderId)
        {
            try
            {
                var order = await repository.GetByIdAsync(orderId); // Отримуємо без AsNoTracking()

                var ser = await serviceCenterPaymentFactory.CreateServiceAsync();
                var tf = await ser.UpdateOrderRequestAndWaitForReceiptAsync(new()
                {
                    OrderIndex = (BigInteger)order.OrderIndex.Value,
                    UserId = order.СustomerId.Value.ToString(),
                    OrderId = order.Id.ToString(),
                    Price = BigInteger.Parse(order.WEIPrice),
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
