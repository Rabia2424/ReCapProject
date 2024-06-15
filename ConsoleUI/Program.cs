using Business.Concrete;
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




        foreach (var car in carManager.GetAll().Data)
        {
            Console.WriteLine(car.BrandId + " " + car.ColorId + " " + car.CarName + " " + car.ModelYear + " " + car.DailyPrice);
        }

        Console.WriteLine("\n");


        //foreach (var car in carManager.GetCarsByBrandId(1))
        //{
        //    Console.WriteLine(car.CarId + " " + car.ModelYear + " " + car.DailyPrice);
        //}

        //Console.WriteLine("\n");


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

        foreach (var car in carManager.GetCarDetails().Data)
        {
            Console.WriteLine(car.CarName + " " + car.BrandName + " " + car.ColorName + " " + car.DailyPrice);
        }
    }
}