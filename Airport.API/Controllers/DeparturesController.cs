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
        public IActionResult Get(int id)
        {
            var result = _departureService.GetById(id);
            return Ok(result);
        }

        // GET api/departures
        [HttpGet]
        public IActionResult Get()
        {
            var results = _departureService.GetAll();
            return Ok(results);
        }

        // POST api/departures
        [HttpPost]
        public IActionResult Post([FromBody] EditableDepartureFields createDepartureRequest)
        {
            var insertedId = _departureService.Insert(createDepartureRequest);
            var result = _departureService.GetById(insertedId);

            var url = Url.Link("GetDeparture", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/departures/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditableDepartureFields updateDepartureRequest)
        {
            var success = _departureService.Update(id, updateDepartureRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/departures/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var success = _departureService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}