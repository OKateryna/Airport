using System;
using Airport.BL.Dto.Plane;
using FluentValidation;

namespace Airport.API.ValidationRules
{
    public class PlaneValidator : AbstractValidator<EditablePlaneFields>
    {
        public PlaneValidator()
        {
            RuleFor(plane => plane.ManufectureDate).NotNull().GreaterThanOrEqualTo(DateTime.Now.AddYears(-20));
            RuleFor(plane => plane.PlaneTypeId).NotEqual(0);
            RuleFor(plane => plane.PlaneName).NotNull().NotEmpty();
            RuleFor(plane => plane.LifeSpan).GreaterThanOrEqualTo(0);
        }
    }
}