using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
            if(result == null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.CarRentalAdded);
            }
            else
            {
                return new ErrorResult(Messages.CarRentalNotAdded);
            }
        }
    }
}
