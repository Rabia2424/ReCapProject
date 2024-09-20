using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmailVerificationService
    {
        IEmailVerificationDal _emailVerificationDal;

        public EmailVerificationService(IEmailVerificationDal emailVerificationDal)
        {
            _emailVerificationDal = emailVerificationDal;
        }

        public bool VerifyToken(string token)
        {
            var emailToken = _emailVerificationDal.Get(t => t.Token.ToString() == token && !t.IsUsed && t.ExpirationDate > DateTime.Now);

            if (emailToken != null)
            {
                emailToken.IsUsed = true;
                _emailVerificationDal.Update(emailToken);
                return true;
            }

            return false;
        }
    }
}
