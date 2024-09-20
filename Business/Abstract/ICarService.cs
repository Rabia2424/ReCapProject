using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetByCarId(int carId);
        IDataResult<CarDetailDto> GetDetailsByCarId(int carId);
        IDataResult<List<CarDetailDto>> GetCarsByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarsByColorId(int colorId);
        IResult Add(Car car);  
        IResult Delete(Car car);   
        IResult Update(Car car);
        IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null);
        IDataResult<List<CarDetailDto>> GetCarByBrandAndColor(int brandId, int colorId);    
        IResult AddTransactionalTest(Car car);
        IDataResult<List<CarDetailDto>> GetCarsByMinAndMaxPrice(int? minPrice, int? maxPrice);
    }
}
