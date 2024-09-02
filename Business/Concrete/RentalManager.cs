using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId).SingleOrDefault(c => c.ReturnDate == null);
            if (result == null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.CarRentalAdded);
            }
            else
            {
                return new ErrorResult(Messages.CarRentalNotAdded);
            }
        }

        public IResult CheckRental(Rental rental)
        {
            var result = BusinessRules.Run(
                CheckRentalCarId(rental.CarId),
                CheckIfReturnDateIsBeforeRentDate(rental.RentDate, rental.ReturnDate),
                CheckIfThisCarIsAlreadyRentedInSelectedDateRange(rental)
                );
            if (result != null)
            {
                return result;
            }
            return new SuccessResult(Messages.PaymentPageAvailable);
        }

        public IDataResult<Rental> CheckRentalCarId(int carId)
        {
            var result = _rentalDal.Get(r => r.CarId == carId && r.ReturnDate == null);
            if (result != null)
            {
                return new ErrorDataResult<Rental>(Messages.CarIsNotAvailable);
            }
            return new SuccessDataResult<Rental>(Messages.CarIsAvailable);
        }

        public IResult Delete(Rental rental)
        {
            if (rental is null)
            {
                return new ErrorResult(Messages.RentalNotDeleted);
            }
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<Rental> GetRentalById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        private IResult CheckIfThisCarIsAlreadyRentedInSelectedDateRange(Rental rental)
        {
            var results = _rentalDal.GetAll(r => r.CarId == rental.CarId);
            if (results != null)
            {
                foreach (var result in results)
                {
                    if (rental.RentDate < result.RentDate
                || result.ReturnDate == null
                || rental.RentDate < result.ReturnDate)
                    {
                        return new ErrorResult(Messages.ThisCarIsAlreadyRentedInSelectedDateRange);
                    }
                }
            }
            return new SuccessResult(); 
        }

        private IResult CheckIfReturnDateIsBeforeRentDate(DateTime rentDate, DateTime? returnDate)
        {
            if (returnDate.HasValue)
            {
                if (((DateTime)returnDate).Date < rentDate.Date)
                {
                    return new ErrorResult(Messages.ReturnDateIsBeforeRentDate);
                }
            }
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetRentalByCarId(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId);
            if (result.Any())
            {
                return new SuccessDataResult<List<Rental>>(result);
            }
            return new ErrorDataResult<List<Rental>>("There is no rental information");
        }
    }
}
