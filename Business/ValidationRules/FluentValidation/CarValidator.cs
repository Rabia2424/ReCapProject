using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.CarName).NotEmpty();
            RuleFor(c => c.CarName).MinimumLength(2).WithMessage(Messages.CarNotAdded);

            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.DailyPrice).GreaterThan(7000);

            RuleFor(c => c.Description).MinimumLength(20);
            RuleFor(c => c.Description).MaximumLength(800).WithMessage("Car description must be at most 800 characters long.");
        }
    }
}
