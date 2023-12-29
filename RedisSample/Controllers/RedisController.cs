using Microsoft.AspNetCore.Mvc;
using RedisSample.Models;
using RedisSample.Services;
using System.Text.Json;

namespace RedisSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly IRedisService _redisService;
       public RedisController(IRedisService redisService) 
        {
            _redisService = redisService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDataById(int id) 
        {
            var response = await _redisService.Get(id.ToString());
            return Ok(JsonSerializer.Deserialize<Profile>(response));
        }

        [HttpPost]
        public async Task<IActionResult> CreateData(Profile profile) 
        {
            await _redisService.Add(profile.Id.ToString(), JsonSerializer.Serialize(profile));
            return StatusCode(StatusCodes.Status201Created,profile.Id) ;
        }
    }
}
