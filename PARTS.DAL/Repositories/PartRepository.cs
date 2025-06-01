using Microsoft.EntityFrameworkCore;
using PARTS.DAL.Data;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Interfaces;

namespace PARTS.DAL.Repositories
{
    public class PartRepository : GenericRepository<Part>, IPartRepository
    {
        public PartRepository(PartsDBContext databaseContext)
            : base(databaseContext)
        {
        }
        public async Task<(List<Part> items, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize, string? search = null, Guid? categoryId = null)
        {
            var query = databaseContext.Set<Part>().AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.PartName.Contains(search) ||
                                         p.Description.Contains(search) ||
                                         p.PartNumber.Contains(search));
            }

            if (categoryId.HasValue)
            {
                var categoryIds = await GetCategoryWithDescendantsAsync(categoryId.Value);
                query = query.Where(p => categoryIds.Contains(p.CategoryId.Value));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        private async Task<List<Guid>> GetCategoryWithDescendantsAsync(Guid categoryId)
        {
            var categoryIds = new List<Guid> { categoryId };

            // Get all categories to build hierarchy
            var allCategories = await databaseContext.Set<Category>()
                .Select(c => new { c.Id, c.ParentId })
                .ToListAsync();

            // Recursively find all descendants
            var queue = new Queue<Guid>();
            queue.Enqueue(categoryId);

            while (queue.Count > 0)
            {
                var currentId = queue.Dequeue();
                var children = allCategories
                    .Where(c => c.ParentId == currentId)
                    .Select(c => c.Id)
                    .ToList();

                foreach (var childId in children)
                {
                    if (!categoryIds.Contains(childId))
                    {
                        categoryIds.Add(childId);
                        queue.Enqueue(childId);
                    }
                }
            }

            return categoryIds;
        }
    }
}
