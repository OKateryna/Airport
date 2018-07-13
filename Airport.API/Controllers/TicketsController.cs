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
        public IActionResult Get(int id)
        {
            var result = _ticketService.GetById(id);
            return Ok(result);
        }

        // GET api/tickets
        [HttpGet]
        public IActionResult Get()
        {
            var results = _ticketService.GetAll();
            return Ok(results);
        }

        // POST api/tickets
        [HttpPost]
        public IActionResult Post([FromBody] EditableTicketFields createTicketRequest)
        {
            var insertedId = _ticketService.Insert(createTicketRequest);
            var result = _ticketService.GetById(insertedId);

            var url = Url.Link("GetTicket", new {id = result.Id});

            return Created(url, result);
        }

        // PUT api/tickets/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditableTicketFields updateTicketRequest)
        {
            var success = _ticketService.Update(id, updateTicketRequest);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/tickets/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var success = _ticketService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}