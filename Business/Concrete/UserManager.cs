﻿using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
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
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            if(user is not null)
            {
                _userDal.Add(user);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        public IDataResult<List<User>> GetAll()
        {
            
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UserListed); 
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
            if(result == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(result);

        }

        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(u => u.Id == id);
            if(user != null) {
                return new SuccessDataResult<User>(user);
            }
            else
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }

        public IResult UpdateUserNames(User user)
        {
            var result = GetById(user.Id);
            if (result.Success)
            {
                var updatedUser = result.Data;
                updatedUser.FirstName = user.FirstName;
                updatedUser.LastName = user.LastName;
                updatedUser.Email = user.Email;
                _userDal.Update(updatedUser);
                return new SuccessResult(Messages.UserUpdated);
            }
            else
            {
                return new ErrorResult(result.Message);
            }
            
        }
    }
}
