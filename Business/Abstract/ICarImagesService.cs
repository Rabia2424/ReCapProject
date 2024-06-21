using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImagesService
    {
        IDataResult<List<CarImages>> GetAll();
        IDataResult<List<CarImages>> GetByCarId(int id);
        IDataResult<CarImages> GetCarImagesById(int id);
        IResult Add(IFormFile file, CarImages image);
        IResult Update(IFormFile file, CarImages image);
        IResult Delete(CarImages image);
    }
}
