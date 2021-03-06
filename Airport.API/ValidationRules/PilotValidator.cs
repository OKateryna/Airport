﻿
using System;
using Airport.BL.Dto.Pilot;
using FluentValidation;

namespace Airport.API.ValidationRules
{
    public class PilotValidator : AbstractValidator<EditablePilotFields>
    {
        public PilotValidator()
        {
            RuleFor(pilot => pilot.BirthDate).NotNull().GreaterThanOrEqualTo(DateTime.Now.AddYears(-50)).LessThan(DateTime.Now.AddYears(-20));
            RuleFor(pilot => pilot.FirstName).NotNull().NotEmpty();
            RuleFor(pilot => pilot.SecondName).NotNull().NotEmpty();
            RuleFor(pilot => pilot.Experience).GreaterThanOrEqualTo(0);
        }
    }
}