using Microsoft.EntityFrameworkCore;
using PARTS.DAL.Data;
using PARTS.DAL.Excepstions;
using PARTS.DAL.Interfaces;
using System.Linq.Expressions;

namespace PARTS.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public readonly PartsDBContext databaseContext;
        public readonly DbSet<TEntity> table;
        private static readonly Dictionary<string, IEnumerable<string>> _navigationPropertiesCache = new();

        public GenericRepository(PartsDBContext databaseContext)
        {
            this.databaseContext = databaseContext;
            table = this.databaseContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity?>?> GetAsync()
        {
            var query = IncludeNavigationProperties(table);
            var entities = await query.ToListAsync();

            if (entities == null || !entities.Any())
            {
                throw new EntityNotFoundException("No entities found in this table.");
            }
            return entities;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = IncludeNavigationProperties(table);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var query = IncludeNavigationProperties(table).AsNoTracking();
            var entity = await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

            if (entity == null)
            {
                throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
            }
            return entity;
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            await table.AddAsync(entity);
            await databaseContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            table.Update(entity);
            await databaseContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await table.FindAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
            }
            table.Remove(entity);
            await databaseContext.SaveChangesAsync();
        }

        private IQueryable<TEntity> IncludeNavigationProperties(IQueryable<TEntity> query)
        {
            var cacheKey = typeof(TEntity).FullName;

            if (!_navigationPropertiesCache.TryGetValue(cacheKey, out var cachedProperties))
            {
                var entityType = databaseContext.Model.FindEntityType(typeof(TEntity));
                cachedProperties = entityType?.GetNavigations()
                    .Where(n => ShouldIncludeNavigation(n))
                    .Select(n => n.Name)
                    ?? Enumerable.Empty<string>();

                _navigationPropertiesCache[cacheKey] = cachedProperties;
            }

            foreach (var navigationProperty in cachedProperties)
            {
                query = query.Include(navigationProperty);
            }

            return query;
        }

        // Override цей метод у похідних класах для кастомної логіки включень
        protected virtual bool ShouldIncludeNavigation(Microsoft.EntityFrameworkCore.Metadata.INavigation navigation)
        {
            // За замовчуванням включаємо тільки reference navigation properties (не collections)
            return !navigation.IsCollection;
        }

        // Додатковий метод для складних includes у похідних класах
        protected virtual IQueryable<TEntity> GetQueryWithCustomIncludes()
        {
            return table.AsQueryable();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await databaseContext.SaveChangesAsync();
        }

        public PartsDBContext GetContext() => databaseContext;

        protected static string GetEntityNotFoundErrorMessage(Guid id) =>
            $"{typeof(TEntity).Name} with ID {id} not found.";
    }
}