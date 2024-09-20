using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private IPaymentDal _paymentDal;
        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        [ValidationAspect(typeof(PaymentValidator))]
        public IResult Add(Payment payment)
        {
            
            _paymentDal.Add(payment);
            return new SuccessResult();
        }

        public IResult CheckIfThisCardIsAlreadySavedForThisCustomer(Payment payment)
        {
            var result = _paymentDal.Get(p => p.CustomerId == payment.CustomerId && p.CardNumber == payment.CardNumber);
            if (result != null)
            {
                return new ErrorResult(Messages.ThisCardIsAlreadySaved);
            }
            return new SuccessResult();
        }

        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult();
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IDataResult<List<Payment>> GetAllByCustomerId(int customerId)
        {
            var result = _paymentDal.GetAll(c=>c.CustomerId == customerId);
            return new SuccessDataResult<List<Payment>>(result);
        }

        public IDataResult<List<Payment>> GetAllSavedCardsByCustomer(int customerId)
        {
            var result = _paymentDal.GetAll(p => p.CustomerId == customerId);
            if (!result.Any())
            {
                return new ErrorDataResult<List<Payment>>(Messages.SaveCardNotFound);
            }
            return new SuccessDataResult<List<Payment>>(result);
        }

        public IDataResult<Payment> GetById(int id)
        {
            var result = _paymentDal.Get(p=>p.PaymentId == id);
            return new SuccessDataResult<Payment>(result);
        }

        //[TransactionScopeAspect]
        //[ValidationAspect(typeof(PaymentValidator))]
        public IResult Pay(int customerId)
        {
            var result = _paymentDal.GetAll(p=>p.CustomerId == customerId);    

            if (result.Any())
            {
                return new SuccessResult(Messages.PaymentSuccessfull);
            }
            return new ErrorResult(Messages.PaymentDenied);
        }

        public IResult Update(Payment payment)
        {
            _paymentDal.Update(payment);
            return new SuccessResult(); 
        }
    }
}
