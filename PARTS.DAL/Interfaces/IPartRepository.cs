using PARTS.DAL.Entities.Item;

namespace PARTS.DAL.Interfaces
{
    public interface IPartRepository : IGenericRepository<Part>
    {
        Task<(List<Part> items, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize, string? search = null, Guid? categoryId = null);

    }

}
