using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Crew;
using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrewsController : Controller
    {
        private readonly ICrewService _crewService;
        public CrewsController(ICrewService crewService)
        {
            _crewService = crewService;
        }

        // GET api/crews/5
        [HttpGet("{id}", Name = "GetCrew")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _crewService.GetById(id);
            return Ok(result);
        }

        // GET api/crews
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _crewService.GetAll();
            return Ok(results);
        }

        // POST api/crews
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EditableCrewFields createCrewRequest)
        {
            var insertedId = await _crewService.Insert(createCrewRequest);
            var result = await _crewService.GetById(insertedId);

            var url = Url.Link("GetCrew", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/crews/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditableCrewFields updateCrewRequest)
        {
            var success = await _crewService.Update(id, updateCrewRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/crews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _crewService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}