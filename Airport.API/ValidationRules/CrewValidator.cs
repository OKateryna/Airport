using Airport.BL.Dto.Crew;
using FluentValidation;

namespace Airport.API.ValidationRules
{
    public class CrewValidator : AbstractValidator<EditableCrewFields>
    {
        public CrewValidator()
        {
            RuleFor(crew => crew.PilotId).NotEqual(0);
            RuleFor(crew => crew.Name).NotEmpty();
            RuleFor(crew => crew.StewardessIds).NotNull();
        }
    }
}