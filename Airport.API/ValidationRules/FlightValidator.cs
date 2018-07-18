using System;
using Airport.BL.Dto.Flight;
using FluentValidation;

namespace Airport.API.ValidationRules
{
    public class FlightValidator : AbstractValidator<EditableFlightFields>
    {
        public FlightValidator()
        {
            RuleFor(flight => flight.TicketIds).NotNull().NotEmpty();
            RuleFor(flight => flight.Destination).NotNull().NotEmpty();
            RuleFor(flight => flight.Number).NotNull().NotEmpty();
            RuleFor(flight => flight.PlaceDeparture).NotNull().NotEmpty();
            RuleFor(flight => flight.TimeDeparture).NotNull().GreaterThan(DateTime.Now);
            RuleFor(flight => flight.TimeDestination).NotNull().GreaterThan(DateTime.Now);
        }
    }
}