using Microsoft.EntityFrameworkCore;
using PARTS.DAL.Data;
using PARTS.DAL.Entities;
using PARTS.DAL.Excepstions;
using PARTS.DAL.Interfaces;
using ServiceCenterPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.DAL.Repositories
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(PartsDBContext databaseContext)
            : base(databaseContext)
        {
        }


        public override async Task<Order?> GetByIdAsync(Guid id)
        {
            var query = table.Include(p => p.OrderParts.AsQueryable().AsNoTracking()).ThenInclude(p=>p.Part).AsQueryable().AsNoTracking();
            var entity = await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
            }
            return entity;
        }
        public override async Task<IEnumerable<Order>> GetAsync(Expression<Func<Order, bool>> predicate = null)
        {
            var query = table.AsQueryable();
            query.Include(p => p.OrderParts).ThenInclude(p=>p.Part);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }
        public async Task AddPartToOrderAsync(Guid orderId, Guid partId, int quantity)
        {
            try
            {
                var order = databaseContext.Orders.Include(p => p.OrderParts).ThenInclude(p=>p.Part).ToList().FirstOrDefault(p => p.Id == orderId);
                if (order == null) throw new EntityNotFoundException($"order {orderId} not found");

                var part = await databaseContext.Parts.FindAsync(partId);
                if (part == null) throw new EntityNotFoundException($"part {partId} not found");

                order.OrderParts.Add(new() {Order = order,Part = part, Quantity = quantity});
                var price = order.OrderParts.Sum(p => p.Part.PriceRegular) ?? 0;
                var weiprice = await EthereumPriceConverter.ConvertUsdToEtherAsync(price, 18);
                order.WEIPrice = weiprice.ToString();
                await databaseContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e; 
            }
        }


        public async Task RemovePartFromOrderAsync(Guid orderId, Guid partId)
        {
            try
            {
                var order = databaseContext.Orders.Include(p => p.OrderParts).ThenInclude(p=>p.Part).ToList().FirstOrDefault(p => p.Id == orderId);
                
                if (order == null) throw new EntityNotFoundException($"order {orderId} not found");

                var part = await databaseContext.Parts.FindAsync(partId);
                if (part == null) throw new EntityNotFoundException($"part {partId} not found");
                 var entt = order.OrderParts.Where(p=>p.Order == order&&p.Part == part).FirstOrDefault();
                if (entt == null)
                    throw new Exception("");
                order.OrderParts.Remove(entt);
                var price = order.OrderParts.Sum(p => p.Part.PriceRegular) ?? 0;
                var weiprice = await EthereumPriceConverter.ConvertUsdToEtherAsync(price, 18); 
                order.WEIPrice = weiprice.ToString(); 
                await databaseContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e; 
            }
        }
    }
}
