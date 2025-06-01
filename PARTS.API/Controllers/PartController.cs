using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PARTS.API.Helpers;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using System.Text;

namespace PARTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Mechanic,User")]
    public class PartController : ControllerBase
    {
        private readonly ILogger<PartController> _logger;
        private readonly IDistributedCache distributedCache;
        private readonly IPartService partService;
        public PartController(
            ILogger<PartController> logger,
             IDistributedCache distributedCache
,
             IPartService PartService
             )
        {
            _logger = logger;
            this.distributedCache = distributedCache;
            this.partService = PartService;
        }
        [HttpGet("paginated")]
        public async Task<ActionResult<Pagination<PartResponse>>> GetPaginatedAsync(
            [FromQuery] PaginationParams paginationParams,
            [FromQuery] string? search,
            [FromQuery] Guid? categoryId)
        {
            try
            {
                var paginatedResult = await partService.GetPaginatedAsync(
                    paginationParams.PageNumber,
                    paginationParams.PageSize,
                    search,
                    categoryId);

                _logger.LogInformation($"PartController GetPaginatedAsync - Page {paginationParams.PageNumber}");
                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //  [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartResponse>>> GetAllAsync()
        {
            try
            {
                var cacheKey = "PartList";
                string serializedList;
                var list = new List<PartResponse>();
                byte[]? redisList = null;

                try
                {
                    redisList = await distributedCache.GetAsync(cacheKey);
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"Failed to retrieve data from cache: {e.Message}");
                }

                if (redisList != null)
                {
                    serializedList = Encoding.UTF8.GetString(redisList);
                    list = JsonConvert.DeserializeObject<List<PartResponse>>(serializedList);
                }
                else
                {
                    list = (List<PartResponse>)await partService.GetAllAsync();
                    try
                    {
                        serializedList = JsonConvert.SerializeObject(list);
                        redisList = Encoding.UTF8.GetBytes(serializedList);
                        var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(1));
                        await distributedCache.SetAsync(cacheKey, redisList, options);
                    }
                    catch (Exception e)
                    {
                        _logger.LogWarning($"Failed to set data to cache: {e.Message}");
                    }
                }

                _logger.LogInformation($"PartController GetAllAsync");
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        //  [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult<PartResponse>> GetByIdAsync(Guid Id)
        {
            try
            {
                var result = await partService.GetByIdAsync(Id);
                if (result == null)
                {
                    _logger.LogInformation($"Part із Id: {Id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали Part з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //  [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] PartRequest brand)
        {
            try
            {
                if (brand == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт Part є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт Part є некоректним");
                }
                var res = await partService.PostAsync(brand);


                return Created("", res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //  [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] PartRequest brand)
        {
            try
            {
                if (brand == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт Part є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт Part є некоректним");
                }

                await partService.UpdateAsync(brand);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі UpdateAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //   [Authorize]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var event_entity = await partService.GetByIdAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"Запис Part із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await partService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
