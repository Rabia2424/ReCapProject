using Business.Concrete;
using DataAccess.Concrete;
using Entities.Concrete;

internal class Program
{
    private static void Main(string[] args)
    {
        CarManager carManager = new CarManager(new InMemoryCarDal());
        foreach (var car in carManager.GetAll())
        {
            Console.WriteLine(car.CarId + " " + car.ModelYear + " " + car.DailyPrice);
        }

        Console.WriteLine("\n");

        carManager.Delete(new Car { CarId = 5 });
        foreach (var car in carManager.GetAll())
        {
            Console.WriteLine(car.CarId + " " + car.ModelYear + " " + car.DailyPrice);
        }

        Console.WriteLine("\n");

        carManager.Insert(new Car
        {
            CarId = 5,
            BrandId = 5,
            ColorId = 5,
            DailyPrice = 10000,
            ModelYear = 2023
        });

        foreach(var car in carManager.GetAll())
        {
            Console.WriteLine(car.CarId + " " + car.ModelYear + " " + car.DailyPrice);
        }
    }
}