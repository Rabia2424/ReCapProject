using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class EmailVerificationToken : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Token { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
