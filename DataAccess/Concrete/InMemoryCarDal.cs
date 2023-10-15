using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class InMemoryCarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{CarId=1, BrandId=1, ColorId=1, DailyPrice=6000, ModelYear=2020, Description="Description"},
                new Car{CarId=2, BrandId=2, ColorId=2, DailyPrice=8000, ModelYear=2022, Description="Description"},
                new Car{CarId=3, BrandId=3, ColorId=3, DailyPrice=8000, ModelYear=2020, Description="Description"},
                new Car{CarId=4, BrandId=4, ColorId=4, DailyPrice=6500, ModelYear=2022, Description="Description"},
                new Car{CarId=5, BrandId=5, ColorId=5, DailyPrice=9000, ModelYear=2023, Description="Description"},
            };
        }
        public void Delete(Car car)
        {
            var carToDelete = _cars.FirstOrDefault(c=>c.CarId == car.CarId);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(c=>c.CarId == id).ToList();
        }

        public void Insert(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            var carToUpdate = _cars.FirstOrDefault(c => c.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;  
        }
    }
}
