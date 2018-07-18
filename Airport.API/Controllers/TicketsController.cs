using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET api/tickets/5
        [HttpGet("{id}", Name = "GetTicket")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _ticketService.GetById(id);
            return Ok(result);
        }

        // GET api/tickets
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _ticketService.GetAll();
            return Ok(results);
        }

        // POST api/tickets
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EditableTicketFields createTicketRequest)
        {
            var insertedId = await _ticketService.Insert(createTicketRequest);
            var result = await _ticketService.GetById(insertedId);

            var url = Url.Link("GetTicket", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/tickets/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditableTicketFields updateTicketRequest)
        {
            var success = await _ticketService.Update(id, updateTicketRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/tickets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _ticketService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}