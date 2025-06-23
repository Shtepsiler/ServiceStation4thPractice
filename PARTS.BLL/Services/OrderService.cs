using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Data;
using PARTS.DAL.Entities;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Interfaces;
using ServiceCenterPayment;
using System.Numerics;

namespace PARTS.BLL.Services
{
    public class OrderService : GenericService<Order, OrderRequest, OrderResponse>, IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IPartRepository partRepository;
        private readonly IServiceCenterPaymentServiceFactory serviceCenterPaymentFactory;
        private readonly PartsDBContext context;

        public OrderService(IPartRepository partrepository, IOrderRepository repository, IMapper mapper, IServiceCenterPaymentServiceFactory serviceCenterPayment, PartsDBContext context)
            : base(repository, mapper)
        {
            partRepository = partrepository;
            this.repository = repository;
            serviceCenterPaymentFactory = serviceCenterPayment;
            this.context = context;
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

        public async Task AddPartToOrderAsync(Guid orderId, Guid partId, int quantity)
        {
            await repository.AddPartToOrderAsync(orderId, partId, quantity);

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

        public async Task<IEnumerable<PartResponse>> AddPartsByCategoriesAsync(AddPartsByCategoryRequest request)
        {
            var parts = new List<Part>();
            var categoryParts = await partRepository.GetAsync();
            foreach (var categoryName in request.Categories)
            {
                var catParts = categoryParts.Where(p =>
                    p.Category.Title == categoryName);
                var selectedPart = catParts.FirstOrDefault();
                if (selectedPart != null)
                {
                    parts.Add(selectedPart);
                }
            }

            // Завантажи order з OrderParts з бази даних
            var order = await repository.GetByIdAsync(request.OrderId);
            if (order != null)
            {
                foreach (var part in parts)
                {
                    // Перевір існування OrderPart безпосередньо в базі даних
                    var existingOrderPart = await context.OrdersParts
                        .FirstOrDefaultAsync(op => op.OrderId == request.OrderId && op.PartId == part.Id);

                    if (existingOrderPart != null)
                    {
                        // OrderPart існує в базі, оновлюємо кількість
                        existingOrderPart.Quantity += 1;
                        context.OrdersParts.Update(existingOrderPart);
                    }
                    else
                    {
                        // OrderPart не існує, створюємо новий
                        var newOrderPart = new OrderPart
                        {
                            OrderId = order.Id,
                            PartId = part.Id,
                            Quantity = 1
                        };
                        await context.OrdersParts.AddAsync(newOrderPart);
                    }
                }

                // Збереги зміни
                await context.SaveChangesAsync();

                // Trigger price update event
                if (OnPriceUpdated != null)
                {
                    await OnPriceUpdated(order.Id);
                }
            }

            return parts.Select(p => new PartResponse
            {
                Id = p.Id,
                PartNumber = p.PartNumber,
                ManufacturerNumber = p.ManufacturerNumber,
                Description = p.Description,
                PartName = p.PartName,
                IsUniversal = p.IsUniversal,
                PriceRegular = p.PriceRegular,
                PartTitle = p.PartTitle,
                PartAttributes = p.PartAttributes,
                IsMadeToOrder = p.IsMadeToOrder,
                FitNotes = p.FitNotes,
                Count = p.Count,
                CategoryId = p.CategoryId
            });
        }




        private readonly SemaphoreSlim _semaphore = new(1, 1);

        private async Task ChangePrice(Guid orderId)
        {
            await _semaphore.WaitAsync();
            try
            {
                var order = await repository.GetByIdAsync(orderId);
                var ser = await serviceCenterPaymentFactory.CreateServiceAsync();

                var tf = await ser.UpdateOrderRequestAndWaitForReceiptAsync(new()
                {
                    OrderIndex = (BigInteger)order.OrderIndex.Value,
                    UserId = order.СustomerId.Value.ToString(),
                    OrderId = order.Id.ToString(),
                    Price = BigInteger.Parse(order.WEIPrice),
                });
            }
            finally
            {
                _semaphore.Release();
            }
        }
        public async Task<IEnumerable<PartResponse>> GetPartsByOrderId(Guid orderId)
        {
            var order = await repository.GetByIdAsync(orderId);

            return order.OrderParts.Select(op => new PartResponse
            {
                Id = op.Part.Id,
                PartNumber = op.Part.PartNumber,
                ManufacturerNumber = op.Part.ManufacturerNumber,
                Description = op.Part.Description,
                PartName = op.Part.PartName,
                IsUniversal = op.Part.IsUniversal,
                PriceRegular = op.Part.PriceRegular,
                PartTitle = op.Part.PartTitle,
                PartAttributes = op.Part.PartAttributes,
                IsMadeToOrder = op.Part.IsMadeToOrder,
                FitNotes = op.Part.FitNotes,
                Count = op.Part.Count,
                CategoryId = op.Part.CategoryId,
                OrderedCount = op.Quantity
            }).ToList();
        }


    }
}
