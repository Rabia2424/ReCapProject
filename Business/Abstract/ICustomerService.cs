﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<CustomerDto>> GetAll();
        IResult Add(CustomerDto customerDto);
        IDataResult<Customer> GetCustomerByUserId(int userId);
        IResult Update(CustomerDto customerDto);
        IResult Delete(CustomerDto customerDto);  
    }
}
