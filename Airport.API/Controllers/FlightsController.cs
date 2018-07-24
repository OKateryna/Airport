using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Flight;
using Airport.BL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : Controller
    {
        private readonly IFlightService _flightService;
        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        // GET api/flights/5
        [HttpGet("{id}", Name = "GetFlight")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _flightService.GetById(id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        // GET api/flights
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _flightService.GetFlightsWithDelay();
            return Ok(results);
        }

        // POST api/flights
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EditableFlightFields createFlightRequest)
        {
            var insertedId = await _flightService.Insert(createFlightRequest);
            var result = await _flightService.GetById(insertedId);

            var url = Url.Link("GetFlight", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/flights/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditableFlightFields updateFlightRequest)
        {
            var success = await _flightService.Update(id, updateFlightRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/flights/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _flightService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}