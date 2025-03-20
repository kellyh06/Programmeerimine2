using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KooliProjekt.Controllers
{
    [Route("api/MusicTracks")]
    [ApiController]
    public class MusicTracksApiController : ControllerBase
    {
        private readonly IMusicTrackService _service;

        public MusicTracksApiController(IMusicTrackService service)
        {
            _service = service;
        }

        // GET: api/<MusicTracksApiController>
        [HttpGet]
        public async Task<IEnumerable<MusicTrack>> Get()
        {
            var result = await _service.List(1, 10000, null);
            return result.Results;
        }

        // GET api/<MusicTracksApiController>/5
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

        // POST api/<MusicTracksApiController>
        [HttpPost]
        public async Task<object> Post([FromBody] MusicTrack list)
        {
            await _service.Save(list);

            return Ok(list);
        }

        // PUT api/<MusicTracksApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MusicTrack list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            await _service.Save(list);

            return Ok();
        }

        // DELETE api/<MusicTracksApiController>/5
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
