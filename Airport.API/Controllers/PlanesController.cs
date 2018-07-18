using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Plane;
using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesController : Controller
    {
        private readonly IPlaneService _planeService;
        public PlanesController(IPlaneService planeService)
        {
            _planeService = planeService;
        }

        // GET api/planes/5
        [HttpGet("{id}", Name = "GetPlane")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _planeService.GetById(id);
            return Ok(result);
        }

        // GET api/planes
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _planeService.GetAll();
            return Ok(results);
        }

        // POST api/planes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EditablePlaneFields createPlaneRequest)
        {
            var insertedId = await _planeService.Insert(createPlaneRequest);
            var result = await _planeService.GetById(insertedId);

            var url = Url.Link("GetPlane", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/planes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditablePlaneFields updatePlaneRequest)
        {
            var success = await _planeService.Update(id, updatePlaneRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/planes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _planeService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}