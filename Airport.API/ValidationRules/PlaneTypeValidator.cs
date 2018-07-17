
using Airport.BL.Dto.PlaneType;
using FluentValidation;

namespace Airport.API.ValidationRules
{
    public class PlaneTypeValidator : AbstractValidator<EditablePlaneTypeFields>
    {
        public PlaneTypeValidator()
        {
            RuleFor(planeType => planeType.CarryingCapacity).GreaterThan(0);
            RuleFor(planeType => planeType.PlacesAmount).GreaterThanOrEqualTo(1);
            RuleFor(planeType => planeType.PlaneModel).NotNull().NotEmpty();
        }
    }
}