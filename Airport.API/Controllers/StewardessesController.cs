using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Stewardess;
using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StewardessesController : Controller
    {
        private readonly IStewardessService _stewardessService;
        public StewardessesController(IStewardessService stewardessService)
        {
            _stewardessService = stewardessService;
        }

        // GET api/stewardesss/5
        [HttpGet("{id}", Name = "GetStewardess")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _stewardessService.GetById(id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        // GET api/stewardesss
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _stewardessService.GetAll();
            
            return Ok(results);
        }

        // POST api/stewardesss
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EditableStewardessFields createStewardessRequest)
        {
            var insertedId = await _stewardessService.Insert(createStewardessRequest);
            var result = await _stewardessService.GetById(insertedId);

            var url = Url.Link("GetStewardess", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/stewardesss/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditableStewardessFields updateStewardessRequest)
        {
            var success = await _stewardessService.Update(id, updateStewardessRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/stewardesss/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _stewardessService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}