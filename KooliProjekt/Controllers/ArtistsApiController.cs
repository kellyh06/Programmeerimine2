using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KooliProjekt.Controllers
{
    [Route("api/Artists")]
    [ApiController]
    public class ArtistsApiController : ControllerBase
    {
        private readonly IArtistService _service;

        public ArtistsApiController(IArtistService service)
        {
            _service = service;
        }

        // GET: api/<ArtistsApiController>
        [HttpGet]
        public async Task<IEnumerable<Artist>> Get()
        {
            var result = await _service.List(1, 10000);
            return result.Results;
        }

        // GET api/<ArtistsApiController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var list = await _service.Get(id);
            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // POST api/<ArtistsApiController>
        [HttpPost]
        public async Task<object> Post([FromBody] Artist list)
        {
            await _service.Save(list);

            return Ok(list);
        }

        // PUT api/<ArtistsApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Artist list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            await _service.Save(list);

            return Ok();
        }

        // DELETE api/<ArtistsApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var list = await _service.Get(id);
            if (list == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return Ok();
        }
    }
}
