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
        public IActionResult Get(int id)
        {
            var result = _crewService.GetById(id);
            return Ok(result);
        }

        // GET api/crews
        [HttpGet]
        public IActionResult Get()
        {
            var results = _crewService.GetAll();
            return Ok(results);
        }

        // POST api/crews
        [HttpPost]
        public IActionResult Post([FromBody] EditableCrewFields createCrewRequest)
        {
            var insertedId = _crewService.Insert(createCrewRequest);
            var result = _crewService.GetById(insertedId);

            var url = Url.Link("GetCrew", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/crews/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditableCrewFields updateCrewRequest)
        {
            var success = _crewService.Update(id, updateCrewRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/crews/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var success = _crewService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}