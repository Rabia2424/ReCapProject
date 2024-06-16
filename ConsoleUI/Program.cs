using Business.Concrete;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

internal class Program
{
    private static void Main(string[] args)
    {
        // This area is important bcs in this way I added InMemoryCars to database

        //InMemoryCarDal InMemory = new InMemoryCarDal();

        CarManager carManager = new CarManager(new EfCarDal());
        //foreach (var entity in InMemory.GetAll())
        //{
        //   carManager.Add(entity);
        //}

        var carsResult = carManager.GetAll();

        if (carsResult.Success)
        {
            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.BrandId + " " + car.ColorId + " " + car.CarName + " " + car.ModelYear + " " + car.DailyPrice);
            }
        }
        else
        {
            Console.WriteLine(carsResult.Message);
        }
      

        Console.WriteLine("\n");


        //foreach (var car in carManager.GetCarsByBrandId(1))
        //{
        //    Console.WriteLine(car.CarId + " " + car.ModelYear + " " + car.DailyPrice);
        //}

        //Console.WriteLine("\n");



        //carManager.Update(new Car
        //{
        //    CarId = 1,
        //    BrandId = 1,
        //    ColorId = 1,
        //    CarName = "Volvo",
        //    ModelYear = 2021,
        //    DailyPrice = 15000,
        //    Description = "description"
        //});
        //Console.WriteLine("Car Updated!");

        var carDetailsResult = carManager.GetCarDetails();

        if (carDetailsResult.Success)
        {
            foreach (var car in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(car.CarName + " " + car.BrandName + " " + car.ColorName + " " + car.DailyPrice);
            }
        }
        else
        {
            Console.WriteLine(carDetailsResult.Message);
        }

        Console.WriteLine("\n");    


        BrandManager brandManager = new BrandManager(new EfBrandDal());
        //var result = brandManager.Add(new Brand
        //            {
        //                BrandId = 7,
        //                BrandName = "Nissan"
        //            });

        //if(result.Success)
        //{
        //    Console.WriteLine(result.Message);
        //}
        //else
        //{
        //    Console.WriteLine(result.Message);
        //}


    }
}