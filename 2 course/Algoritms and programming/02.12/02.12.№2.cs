using System;
using System.Collections.Generic;

struct CarInfo
{
    public string Brand;
    public int Year;
    public CarInfo(string brand, int year) { Brand = brand; Year = year; }
}

class Car
{
    public int Id { get; set; }
    public CarInfo Info { get; set; }
    public bool IsClean { get; set; }
    
    private Car(int id, CarInfo info, bool isClean)
    {
        Id = id;
        Info = info;
        IsClean = isClean;
    }
    
    public static Car? Create(int id, CarInfo info, bool isClean)
    {
        if (info.Year.GetType() != typeof(int)) return null;
        return new Car(id, info, isClean);
    }
}

class Garage
{
    public List<Car> Cars { get; set; }
    
    public Garage()
    {
        Cars = new List<Car>();
    }
    
    public void AddCar(Car car)
    {
        Cars.Add(car);
    }
}

class Wash
{
    public static void WashCar(Car car)
    {
        if (car != null)
        {
            car.IsClean = true;
            Console.WriteLine($"car {car.Info.Brand} washed");
        }
    }
}

class Program
{
    public static void Main()
    {
        Garage garage = new Garage();
        
        Console.WriteLine("inp cars count");
        int n = Convert.ToInt32(Console.ReadLine());
        
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("inp car id");
            int cid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("inp car brand");
            string cbrand = Console.ReadLine();
            Console.WriteLine("inp car year");
            int cyear = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("inp car state (true - clean, false - dirty)");
            bool cIsClean = Convert.ToBoolean(Console.ReadLine());
            
            CarInfo cinfo = new CarInfo(cbrand, cyear);
            Car car = Car.Create(cid, cinfo, cIsClean);
            garage.AddCar(car);
        }
        
        Console.WriteLine("start wash cycle:");
        Console.WriteLine("1 - 3 steps");
        
        for (int step = 1; step <= 3; step++)
        {
            Console.WriteLine($"\nstep {step}:");
            
            foreach (Car car in garage.Cars)
            {
                if (car.IsClean)
                {
                    Console.WriteLine($"car {car.Info.Brand} clean -> becomes dirty");
                    car.IsClean = false;
                }
                else
                {
                    Console.WriteLine($"car {car.Info.Brand} dirty -> go to wash");
                    Wash.WashCar(car);
                }
                
                Console.WriteLine($"car {car.Info.Brand} state: {(car.IsClean ? "clean" : "dirty")}");
            }
        }
    }
}