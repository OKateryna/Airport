using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Pilot;
using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotsController : Controller
    {
        private readonly IPilotService _pilotService;
        public PilotsController(IPilotService pilotService)
        {
            _pilotService = pilotService;
        }

        // GET api/pilots/5
        [HttpGet("{id}", Name = "GetPilot")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _pilotService.GetById(id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        // GET api/pilots
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _pilotService.GetAll();
            return Ok(results);
        }

        // POST api/pilots
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EditablePilotFields createPilotRequest)
        {
            var insertedId = await _pilotService.Insert(createPilotRequest);
            var result = await _pilotService.GetById(insertedId);

            var url = Url.Link("GetPilot", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/pilots/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditablePilotFields updatePilotRequest)
        {
            var success = await _pilotService.Update(id, updatePilotRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/pilots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _pilotService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}