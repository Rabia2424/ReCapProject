using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TokenService
    {
        IEmailVerificationDal _emailVerificationDal;

        public TokenService(IEmailVerificationDal emailVerificationDal)
        {
            _emailVerificationDal = emailVerificationDal;
        }

        public void SaveToken(int userId, string token, DateTime expirationDate)
        {
            var emailToken = new EmailVerificationToken
            {
                UserId = userId,
                Token = Guid.Parse(token),
                ExpirationDate = expirationDate
            };

            _emailVerificationDal.Add(emailToken);
        }
    }

    public class TokenGenerator
    {
        public string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }
    }

}
