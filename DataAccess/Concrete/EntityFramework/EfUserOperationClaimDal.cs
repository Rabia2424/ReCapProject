using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, ReCapProjectContext>, IUserOperationClaimDal
    {
        public List<UserOperationClaimDto> GetAllUserClaimsWithDetails()
        {
            using(ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from uoc in context.UserOperationClaims
                             join u in context.Users
                             on uoc.UserId equals u.Id
                             join oc in context.OperationClaims
                             on uoc.OperationClaimId equals oc.Id
                             group new {uoc,u,oc } by uoc.UserId into g
                             select new UserOperationClaimDto
                             {
                                 Id = g.First().uoc.Id,
                                 UserId = g.Key,
                                 UserFirstName = g.First().u.FirstName,
                                 UserLastName = g.First().u.LastName,
                                 OperationClaimNames = g.Select(x => x.oc.Name).ToList()
                             };
                return result.ToList();
            }
        }
    }
}
