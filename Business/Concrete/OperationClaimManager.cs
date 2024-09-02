using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _operationClaim;
        public OperationClaimManager(IOperationClaimDal operationClaim)
        {
            _operationClaim = operationClaim;
        }
        public IResult Add(OperationClaim operationClaim)
        {
            _operationClaim.Add(operationClaim);
            return new SuccessResult(Messages.OperationClaimAdded);
        }

        public IResult Delete(OperationClaim operationClaim)
        {
            if(operationClaim == null)
            {
                return new ErrorResult("Operation claim is empty");
            }
            _operationClaim.Delete(operationClaim);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaim.GetAll());
        }

        public IDataResult<OperationClaim> GetById(int operationClaimId)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaim.Get(o=>o.Id == operationClaimId));
        }

        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaim.Update(operationClaim);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }
    }
}
