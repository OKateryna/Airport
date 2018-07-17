using System;
using Airport.BL.Dto.Crew;
using Airport.BL.Dto.Departure;
using FluentValidation;

namespace Airport.API.ValidationRules
{
    public class DepartureValidator : AbstractValidator<EditableDepartureFields>
    {
        public DepartureValidator()
        {
            RuleFor(departure => departure.CrewId).NotEqual(0);
            RuleFor(departure => departure.DepartureDate).NotNull().NotEqual(DateTime.MinValue);
            RuleFor(departure => departure.FlightId).NotEqual(0);
            RuleFor(departure => departure.PlaneId).NotEqual(0);
        }
    }
}