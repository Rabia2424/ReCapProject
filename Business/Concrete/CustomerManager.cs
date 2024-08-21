using AutoMapper;
using Business.Abstract;
using Business.Constants;
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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        IMapper _mapper;

        public CustomerManager(ICustomerDal customerDal, IMapper mapper)
        {
            _customerDal = customerDal;
            _mapper = mapper;
        }

        public IResult Add(CustomerDto customerDto)
        {
            if(customerDto != null)
            {
                var customer = _mapper.Map<Customer>(customerDto);
                _customerDal.Add(customer);
                return new SuccessResult(Messages.CustomerAdded);
            }
            else
            {
                return new ErrorResult(Messages.CustomerNotAdded);
            }
          
        }

        public IResult Delete(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _customerDal.Delete(customer);
            return new SuccessResult();
        }

        public IDataResult<List<CustomerDto>> GetAll()
        {
            return new SuccessDataResult<List<CustomerDto>>(_mapper.Map<List<CustomerDto>>(_customerDal.GetAll()), Messages.CustomerListed);
        }

        public IResult Update(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _customerDal.Update(customer);
            return new SuccessResult(); 
        }
    }
}
