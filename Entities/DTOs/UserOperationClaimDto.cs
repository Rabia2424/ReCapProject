using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserOperationClaimDto:IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFirstName {  get; set; }
        public string UserLastName {  get; set; }
        public List<string> OperationClaimNames { get; set; }
    }
}
