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
        public IActionResult Get(int id)
        {
            var result = _stewardessService.GetById(id);
            return Ok(result);
        }

        // GET api/stewardesss
        [HttpGet]
        public IActionResult Get()
        {
            var results = _stewardessService.GetAll();
            return Ok(results);
        }

        // POST api/stewardesss
        [HttpPost]
        public IActionResult Post([FromBody] EditableStewardessFields createStewardessRequest)
        {
            var insertedId = _stewardessService.Insert(createStewardessRequest);
            var result = _stewardessService.GetById(insertedId);

            var url = Url.Link("GetStewardess", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/stewardesss/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditableStewardessFields updateStewardessRequest)
        {
            var success = _stewardessService.Update(id, updateStewardessRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/stewardesss/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var success = _stewardessService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}