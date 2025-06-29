﻿using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.DAL.Entities;

namespace PARTS.BLL.Services.Interaces
{
    public interface IOrderService : IGenericService<Order, OrderRequest, OrderResponse>
    {
        Task AddPartToOrderAsync(Guid orderId, Guid partId, int quantity);
        Task RemovePartFromOrderAsync(Guid orderId, Guid partId);
        Task<IEnumerable<PartResponse>> GetPartsByOrderId(Guid orderId);
        Task<IEnumerable<PartResponse>> AddPartsByCategoriesAsync(AddPartsByCategoryRequest request);
    }
}
