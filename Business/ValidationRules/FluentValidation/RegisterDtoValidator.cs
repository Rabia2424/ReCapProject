using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RegisterDtoValidator: AbstractValidator<UserForRegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r=>r.FirstName).NotEmpty();
            RuleFor(r => r.FirstName).MinimumLength(2);

            RuleFor(r=>r.LastName).NotEmpty();
            RuleFor(r => r.LastName).MinimumLength(3);

            RuleFor(r=>r.Email).MinimumLength(2);
            RuleFor(r => r.Email).MaximumLength(30);
            RuleFor(r => r.Email).Must(ContainsDotCom);
        }

        private bool ContainsDotCom(string arg)
        {
            return arg.Contains(".com");
        }
    }
}
