using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Departure;
using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeparturesController : Controller
    {
        private readonly IDepartureService _departureService;
        public DeparturesController(IDepartureService departureService)
        {
            _departureService = departureService;
        }

        // GET api/departures/5
        [HttpGet("{id}", Name = "GetDeparture")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _departureService.GetById(id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        // GET api/departures
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _departureService.GetAll();
            return Ok(results);
        }

        // POST api/departures
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EditableDepartureFields createDepartureRequest)
        {
            var insertedId = await _departureService.Insert(createDepartureRequest);
            var result = await _departureService.GetById(insertedId);

            var url = Url.Link("GetDeparture", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/departures/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditableDepartureFields updateDepartureRequest)
        {
            var success = await _departureService.Update(id, updateDepartureRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/departures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _departureService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}