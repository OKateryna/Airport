
using System;
using Airport.BL.Dto.Stewardess;
using FluentValidation;

namespace Airport.API.ValidationRules
{
    public class StewardessValidator : AbstractValidator<EditableStewardessFields>
    {
        public StewardessValidator()
        {
            RuleFor(stewardess => stewardess.BirthDate).NotNull().GreaterThanOrEqualTo(DateTime.Now.AddYears(-45));
            RuleFor(stewardess => stewardess.FirstName).NotNull().NotEmpty();
            RuleFor(stewardess => stewardess.SecondName).NotNull().NotEmpty();
        }
    }
}