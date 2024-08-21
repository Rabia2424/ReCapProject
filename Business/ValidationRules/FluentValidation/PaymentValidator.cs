using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.FullName).NotEmpty();
            RuleFor(p => p.CardNumber).NotEmpty();
            RuleFor(p => p.CardNumber).Length(16);
            RuleFor(p => p.CardNumber).Must(CheckIfContainsOnlyDigits).WithMessage("Card number must consist of digits only.");
            RuleFor(p => p.CVV).NotEmpty();
            RuleFor(p => p.CVV).Length(3);
        }

        private bool CheckIfContainsOnlyDigits(string arg)
        {
            return arg.All(char.IsDigit);
        }
    }
}
