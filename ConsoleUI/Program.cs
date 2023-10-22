using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

internal class Program
{
    private static void Main(string[] args)
    {
        CarManager carManager = new CarManager(new EfCarDal());
        //foreach (var car in carManager.GetAll())
        //{
        //    Console.WriteLine(car.BrandId + " " + car.ColorId + " " + car.CarName + " " + car.ModelYear + " " + car.DailyPrice);
        //}

        //Console.WriteLine("\n");


        foreach (var car in carManager.GetCarsByBrandId(1))
        {
            Console.WriteLine(car.CarId + " " + car.ModelYear + " " + car.DailyPrice);
        }

        Console.WriteLine("\n");


        carManager.Update(new Car
        {
            CarId = 1,  
            BrandId = 1,
            ColorId = 1,
            CarName = "Volvo",
            ModelYear = 2021,
            DailyPrice = 15000,
            Description = "description"
        });
        Console.WriteLine("Car Updated!");

        foreach (var car in carManager.GetCarDetails())
        {
            Console.WriteLine(car.CarName + " " + car.BrandName + " " + car.ColorName + " " + car.DailyPrice);
        }
    }
}