using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private TokenGenerator _tokenGenerator;
        private TokenService _tokenService;
        private EmailService _emailService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, TokenGenerator tokenGenerator, TokenService tokenService, EmailService emailService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _tokenGenerator = tokenGenerator;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
            if (!userToCheck.Success)
            {
                return new ErrorDataResult<User>(userToCheck.Message);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfullLogin);
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);

            var token = _tokenGenerator.GenerateToken();
            _tokenService.SaveToken(user.Id, token, DateTime.UtcNow.AddHours(24));
            await _emailService.SendVerificationEmail(user.Email, token);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> UpdatePassword(UserForPasswordDto userForPasswordDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var result = _userService.GetById(userForPasswordDto.UserId);
            if (!result.Success)
            {
                return new ErrorDataResult<User>(result.Message);
            }

            if (!HashingHelper.VerifyPasswordHash(userForPasswordDto.OldPassword,
                result.Data.PasswordHash, result.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            if(!userForPasswordDto.NewPassword.Equals(userForPasswordDto.RepeatNewPassword))
            {
                return new ErrorDataResult<User>("Repeat password is wrong");
            }

            var updatedUser = result.Data;
            updatedUser.PasswordHash = passwordHash;
            updatedUser.PasswordSalt = passwordSalt;    

            _userService.Update(updatedUser);

            return new SuccessDataResult<User>(updatedUser, Messages.UserPasswordUpdated);
        }

        public IResult UserExistsControlForRegister(string email)
        {
            var result = _userService.GetByEmail(email);
            if (result.Success)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

    }

}
