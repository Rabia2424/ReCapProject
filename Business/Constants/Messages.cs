using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Car added successfully!";
        public static string CarNotAdded = "Car name length must be at least two letters...";
        public static string CarDeleted = "Car deleted successfully!";
        public static string CarUpdated = "Car updated successfully!";
        public static string CarListed = "Cars listed successfully!";
        public static string BrandNotAdded = "Brand name length must be at least three letters";
        public static string BrandAdded = "Brand added successfully!";
        public static string MaintenanceTime = "System under maintenance!";
        public static string BrandNotDeleted = "Brand not deleted!";
        public static string BrandDeleted = "Brand deleted successfully!";
        public static string BrandUpdated = "Brand updated successfully!";
        public static string UserListed = "User listed successfully!";
        public static string CustomerAdded = "Customer added successfully!";
        public static string CustomerNotAdded = "Customer not added!";
        public static string CarRentalAdded = "Car Rental added!";
        public static string CarRentalNotAdded = "Car Rental not added!";
        public static string CustomerListed = "Customer listed successfully!";
        public static string RentalListed = "Rental listed successfully!";
        public static string RentalUpdated = "Rental updated successfully!";
        public static string RentalDeleted = "Rental deleted successfully!";
        public static string RentalNotDeleted = "Rental is empty!";
        public static string ContainsSpecialChar = "Password must contain special char!";
        public static string ContainsBigLetter = "Password must contain big letter!";
        public static string ContainsLetterAndDigit = "Password must contain letter and digit!";
        public static string CarImageNotFound = "Car Image not found!";
        public static string CarImagesDeleted = "Car Image deleted successfully!";
        public static string CarImagesNotDeleted = "An error occurred while deleting the image!";
        public static string CarImagesAdded = "Car Image added successfully!";
        public static string CarImageUpdated = "Car Image updated successfully!";
        public static string AccessTokenCreated = "Access token is created successfully!";
        public static string UserNotFound = "User can not be found!";
        public static string PasswordError = "Password is not verified!";
        public static string SuccessfullLogin = "User login successfully!";
        public static string UserRegistered = "User registered!";
        public static string UserAlreadyExists = "User is already exists!";
        public static string AuthorizationDenied = "Authorization denied!";
        public static string CarIsNotAvailable = "Car is not available to rent!";
        public static string CarIsAvailable = "Car is available to rent!";
        public static string ThisCarIsAlreadyRentedInSelectedDateRange = "This car is already rented in selected date range!";
        public static string ReturnDateIsBeforeRentDate = "Return date can not be before rent date!";
        public static string PaymentPageAvailable = "You are going to shopping cart page!";
        public static string ThisCardIsAlreadySaved = "This cart is already saved for this customer!";
        public static string PaymentSuccessfull = "Payment completed successfully!";
        public static string PaymentDenied = "Payment denied!";
        public static string UserPasswordUpdated = "User password updated successfully!";
        public static string UserUpdated = "User updated successfully!";
    }
}
