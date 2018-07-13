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
        public IActionResult Get(int id)
        {
            var result = _pilotService.GetById(id);
            return Ok(result);
        }

        // GET api/pilots
        [HttpGet]
        public IActionResult Get()
        {
            var results = _pilotService.GetAll();
            return Ok(results);
        }

        // POST api/pilots
        [HttpPost]
        public IActionResult Post([FromBody] EditablePilotFields createPilotRequest)
        {
            var insertedId = _pilotService.Insert(createPilotRequest);
            var result = _pilotService.GetById(insertedId);

            var url = Url.Link("GetPilot", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/pilots/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditablePilotFields updatePilotRequest)
        {
            var success = _pilotService.Update(id, updatePilotRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/pilots/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var success = _pilotService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}