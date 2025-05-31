using AutoMapper;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Interfaces;

namespace PARTS.BLL.Services
{
    public class CategoryService : GenericService<Category, CategoryRequest, CategoryResponse>, ICategoryService
    {
        public CategoryService(ICategoryRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<IEnumerable<CategoryResponse?>?> GetLast()
        {
            try
            {
                var entities = await _repository.GetAsync();
                entities = entities.Where(p => p.SupCategories == null || p.SupCategories.Count == 0);
                return _mapper.Map<IEnumerable<Category?>?, IEnumerable<CategoryResponse?>?>(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
