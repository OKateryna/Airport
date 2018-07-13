using Airport.BL.Abstractions;
using Airport.BL.Dto;
using Airport.BL.Dto.Flight;
using AutoMapper;
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
        public IActionResult Get(int id)
        {
            var result = _flightService.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results = _flightService.GetAll();
            return Ok(results);
        }

        // POST api/flights
        [HttpPost]
        public IActionResult Post([FromBody] EditableFlightFields createFlightRequest)
        {
            var insertedId = _flightService.Insert(createFlightRequest);
            var result = _flightService.GetById(insertedId);

            var url = Url.Link("GetFlight", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/flights/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditableFlightFields updateFlightRequest)
        {
            var success = _flightService.Update(id, updateFlightRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/flights/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var success = _flightService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}