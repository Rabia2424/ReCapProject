using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

internal class Program
{
    private static void Main(string[] args)
    {
        CarManager carManager = new CarManager(new EfCarDal());
        foreach (var car in carManager.GetAll())
        {
            Console.WriteLine(car.BrandId + " " + car.ColorId + " " + car.CarName + " " + car.ModelYear + " " + car.DailyPrice);
        }

        Console.WriteLine("\n");


        foreach (var car in carManager.GetCarsByBrandId(1))
        {
            Console.WriteLine(car.CarId + " " + car.ModelYear + " " + car.DailyPrice);
        }

        Console.WriteLine("\n");


        carManager.Add(new Car
        {
            BrandId = 2,
            ColorId = 3,
            CarName = "Volvo",
            ModelYear = 2022,
            DailyPrice = 12000,
            Description = "description"
        });
        Console.WriteLine("Car Added!");


        foreach (var car in carManager.GetAll())
        {
            Console.WriteLine(car.BrandId + " " + car.ColorId + " " + car.CarName + " " + car.ModelYear + " " + car.DailyPrice);
        }
    }
}