using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.PlaneType;
using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaneTypesController : Controller
    {
        private readonly IPlaneTypeService _planeTypeService;
        public PlaneTypesController(IPlaneTypeService planeTypeService)
        {
            _planeTypeService = planeTypeService;
        }

        // GET api/planeTypes/5
        [HttpGet("{id}", Name = "GetPlaneType")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _planeTypeService.GetById(id);
            return Ok(result);
        }

        // GET api/planeTypes
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _planeTypeService.GetAll();
            return Ok(results);
        }

        // POST api/planeTypes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EditablePlaneTypeFields createPlaneTypeRequest)
        {
            var insertedId = await _planeTypeService.Insert(createPlaneTypeRequest);
            var result = await _planeTypeService.GetById(insertedId);

            var url = Url.Link("GetPlaneType", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/planeTypes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditablePlaneTypeFields updatePlaneTypeRequest)
        {
            var success = await _planeTypeService.Update(id, updatePlaneTypeRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/planeTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _planeTypeService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}