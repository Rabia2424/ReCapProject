using Business.Constants;
using Core.Entities.Concrete;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();

            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).MinimumLength(2);
            RuleFor(u => u.Email).MaximumLength(30);
            RuleFor(u => u.Email).Must(ContainsDotCom);

            //RuleFor(u => u.Password).NotEmpty();
            //RuleFor(u => u.Password).MinimumLength(4);
            //RuleFor(u => u.Password).MaximumLength(20);
            //RuleFor(u => u.Password).Must(ContainsSpecialChar).WithMessage(Messages.ContainsSpecialChar);
            //RuleFor(u => u.Password).Must(ContainsBigLetter).WithMessage(Messages.ContainsBigLetter);
            //RuleFor(u => u.Password).Must(ContainsLetterAndDigit).WithMessage(Messages.ContainsLetterAndDigit);
        }

        private bool ContainsDotCom(string arg)
        {
            return arg.Contains(".com");
        }

        private bool ContainsSpecialChar(string arg)
        {
            char[] charArray = { '#', '|', '$', '%', '&', '_', '+', '-', '!', '?' };
            foreach (char c in charArray)
            {
                if (arg.Contains(c))
                {
                    return arg.Contains(c);
                }
            }
            return false;
        }

        private bool ContainsBigLetter(string arg)
        {
            return arg.Any(char.IsUpper);
        }

        private bool ContainsLetterAndDigit(string arg)
        {
            bool containsLetter = arg.Any(char.IsLetter);
            bool containsDigit = arg.Any(char.IsDigit);
            return containsLetter && containsDigit;
        }
    }
}
