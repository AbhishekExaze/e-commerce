using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repository,
         ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
        {
            var data = await repository.GetAllWithSpecAsync(spec);
            var count = await repository.CountAsync(spec);
            var pagination = new Pagination<T>(pageIndex, pageSize, count, data);
            return Ok(pagination);
        }
    }
}
