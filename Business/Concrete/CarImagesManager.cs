using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImagesManager : ICarImagesService
    {
        ICarImagesDal _carImagesDal;

        public CarImagesManager(ICarImagesDal carImagesDal)
        {
            _carImagesDal = carImagesDal;
        }

        public IResult Add(IFormFile file, CarImages image)
        {
            if (file == null || file.Length == 0)
            {
                return new ErrorResult("File is empty!");
            }

            IResult result = BusinessRules.Run(CheckIfCarImageLimit(image.CarId));

            if (result == null)
            {
                var filePath = Path.Combine("Uploads", "Images", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                image.ImagePath = filePath;
                image.Date = DateTime.Now;
                _carImagesDal.Add(image);

                return new SuccessResult(Messages.CarImagesAdded);
            }
            return result;
        }

        public IDataResult<List<CarImages>> GetByCarId(int carId)
        {
            IResult result = BusinessRules.Run(CheckCarImages(carId));

            if (result == null)
            {
                return new SuccessDataResult<List<CarImages>>(_carImagesDal.GetAll(c=>c.CarId == carId));
            }
            var defaultImage = GetDefaultImage(carId);
            //_carImagesDal.Add(defaultImage.Data.First());
            return new ErrorDataResult<List<CarImages>>(defaultImage.Data);
        }

        public IResult CheckCarImages(int carId)
        {
            var result = _carImagesDal.GetAll(c => c.CarId == carId).Count();
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IDataResult<List<CarImages>> GetDefaultImage(int carId)
        {
            List<CarImages> carImages = new List<CarImages>();
            carImages.Add(new CarImages
            {
                CarId = carId,
                ImagePath= "\\Uploads\\Images\\DefaultImage.jpg",
                Date = DateTime.Now
            });
            return new SuccessDataResult<List<CarImages>>(carImages);
        }

        private IResult CheckIfCarImageLimit(int carId)
        {
            int carImageCount = _carImagesDal.GetAll(c => c.CarId == carId).Count;
            if (carImageCount >= 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public IResult Delete(CarImages image)
        {
            var imageToDelete = _carImagesDal.Get(c => c.Id == image.Id);
            if (imageToDelete == null)
            {
                return new ErrorResult(Messages.CarImageNotFound);
            }

            if (File.Exists(imageToDelete.ImagePath))
            {
                File.Delete(imageToDelete.ImagePath);
                _carImagesDal.Delete(imageToDelete);
                return new SuccessResult(Messages.CarImagesDeleted);
            }
            return new ErrorResult(Messages.CarImagesNotDeleted);
        }

        public IDataResult<List<CarImages>> GetAll()
        {
            var carImages = _carImagesDal.GetAll();
            return new SuccessDataResult<List<CarImages>>(carImages);
        }

        public IDataResult<CarImages> GetCarImagesById(int id)
        {
            var carImage = _carImagesDal.Get(c => c.Id == id);
            if (carImage == null)
                return new ErrorDataResult<CarImages>(Messages.CarImageNotFound);

            return new SuccessDataResult<CarImages>(carImage);
        }

        public IResult Update(IFormFile file, CarImages image)
        {
            var imageToUpdate = _carImagesDal.Get(c => c.Id == image.Id);

            if (file != null && file.Length > 0)
            {
                var newFilePath = Path.Combine("Uploads", "Images", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

                if (File.Exists(imageToUpdate.ImagePath))
                {
                    File.Delete(imageToUpdate.ImagePath);
                }

                // Save new image
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                imageToUpdate.ImagePath = newFilePath;
                imageToUpdate.Date = DateTime.Now;
                _carImagesDal.Update(imageToUpdate);

                return new SuccessResult(Messages.CarImageUpdated);
            }
            return new ErrorResult("Invalid file");
        }
    }
}
