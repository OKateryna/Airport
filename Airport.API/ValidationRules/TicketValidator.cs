using Airport.BL.Dto.Ticket;
using FluentValidation;

namespace Airport.API.ValidationRules
{
    public class TicketValidator : AbstractValidator<EditableTicketFields>
    {
        public TicketValidator()
        {
            RuleFor(ticket => ticket.FlightId).NotEqual(0);
            RuleFor(ticket => ticket.Price).GreaterThan(0);
        }
    }
}