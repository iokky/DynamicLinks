using DynamicLinks.Dal;
using DynamicLinks.Domain.Entity;
using DynamicLinks.Services;
using Microsoft.AspNetCore.Mvc;

namespace DynamicLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private ICacheService<DynamicLinkEntity> _cacheService;
        private readonly DynamicLinksDbContext _db;
        public RedisController(ICacheService<DynamicLinkEntity> cacheService, DynamicLinksDbContext db)
        {
            _cacheService = cacheService;
            _db = db;
        }

        [HttpGet("get/{link}")]
        public IActionResult Get([FromRoute] string link)
        {
            var data = _cacheService.Get(link);
            if (data != null)
            {
                return Ok(data);
            }

            var dbData = _db.DynamicLinks.FirstOrDefault(i => i.ShortLink == link);
            if (dbData != null)
            {
                _cacheService.Set(dbData.ShortLink, dbData);
                return Ok(dbData);
            }
            return BadRequest("Not found");
        }

        [HttpPost("/add")]
        public IActionResult Put([FromBody] DynamicLinkEntity value)
        {
            _cacheService.Set(value.ShortLink, value);
            return Ok();
        }

        [HttpPost("/del/{link}")]
        public IActionResult Del([FromRoute] string link)
        {
            _cacheService.Del(link);
            return Ok();
        }
    }
}
