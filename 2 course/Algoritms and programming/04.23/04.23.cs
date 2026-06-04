using System;
using System.Collections.Generic;
using System.Linq;

public class Airplane
{
    public string Type { get; set; }
    public int TotalSeats { get; set; }
}

public class TicketSale
{
    public string FlightNumber { get; set; }
    public int SoldSeats { get; set; }
}

public class FlightSchedule
{
    public string FlightNumber { get; set; }
    public string AirplaneType { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public string DepartureCity { get; set; }
    public string DestinationCity { get; set; }
}

public class Airport
{
    public List<Airplane> Airplanes { get; set; } = new List<Airplane>();
    public List<TicketSale> Sales { get; set; } = new List<TicketSale>();
    public List<FlightSchedule> Schedule { get; set; } = new List<FlightSchedule>();

    public void ShowFlightsGroupedByType()
    {
        Console.WriteLine("Вылеты по типам самолетов:");
        var groups = Schedule.GroupBy(f => f.AirplaneType);

        foreach (var group in groups)
        {
            Console.WriteLine($"Тип: {group.Key}");
            foreach (var f in group)
                Console.WriteLine($"  [{f.FlightNumber}] {f.DepartureCity} -> {f.DestinationCity} ({f.DepartureTime:hh\\:mm})");
        }
    }

    public void ShowMaxLoadFlights()
    {
        Console.WriteLine("Рейсы с максимальной загрузкой:");
        var counts = Schedule.GroupBy(f => f.AirplaneType).ToDictionary(g => g.Key, g => g.Count());

        var loads = from s in Sales
                    join f in Schedule on s.FlightNumber equals f.FlightNumber
                    select new { f.FlightNumber, Val = (double)s.SoldSeats / counts[f.AirplaneType] };

        if (!loads.Any()) return;
        double max = loads.Max(x => x.Val);

        foreach (var item in loads.Where(x => x.Val == max))
            Console.WriteLine($"Рейс: {item.FlightNumber}, Загрузка: {item.Val:F2}");
    }

    public void ShowAverageLoadByType()
    {
        Console.WriteLine("Средняя загрузка по типам:");
        var counts = Schedule.GroupBy(f => f.AirplaneType).ToDictionary(g => g.Key, g => g.Count());

        var avgLoads = from s in Sales
                       join f in Schedule on s.FlightNumber equals f.FlightNumber
                       group (double)s.SoldSeats / counts[f.AirplaneType] by f.AirplaneType into g
                       select new { Type = g.Key, Average = g.Average() };

        foreach (var item in avgLoads)
            Console.WriteLine($"{item.Type}: {item.Average:F2}");
    }

    public void ShowAirplanesByDepartureCity()
    {
        Console.WriteLine("Самолеты, сгруппированные по пунктам вылета:");
        var departureGroups = Schedule.GroupBy(f => f.DepartureCity);

        foreach (var group in departureGroups)
        {
            Console.WriteLine($"Город вылета: {group.Key}");
            var planes = group.Select(f => f.AirplaneType).Distinct();
            foreach (var plane in planes)
            {
                Console.WriteLine($"  - {plane}");
            }
        }
    }

    public void ShowAirplanesByDestinationCity()
    {
        Console.WriteLine("Самолеты, сгруппированные по пунктам назначения:");
        var destinationGroups = Schedule.GroupBy(f => f.DestinationCity);

        foreach (var group in destinationGroups)
        {
            Console.WriteLine($"Город назначения: {group.Key}");
            var planes = group.Select(f => f.AirplaneType).Distinct();
            foreach (var plane in planes)
            {
                Console.WriteLine($"  - {plane}");
            }
        }
    }


    public void ShowFlightsByHour()
    {
        Console.WriteLine("Расписание по часам:");
        var hours = Schedule.GroupBy(f => f.DepartureTime.Hours).OrderBy(g => g.Key);

        foreach (var group in hours)
        {
            Console.WriteLine($"Час {group.Key}:00");
            foreach (var f in group.OrderBy(f => f.DepartureTime.Minutes))
                Console.WriteLine($"  {f.DepartureTime:hh\\:mm} — {f.FlightNumber} ({f.AirplaneType})");
        }
    }
}


public class Program
{
    public static void Main()
    {
        var airport = new Airport();
        airport.Airplanes.AddRange(new[] {
            new Airplane { Type = "Boeing 737", TotalSeats = 160 },
            new Airplane { Type = "Superjet 100", TotalSeats = 98 }
        });

        airport.Schedule.AddRange(new[] {
            new FlightSchedule { FlightNumber = "SU-101", AirplaneType = "Boeing 737", DepartureTime = new TimeSpan(10, 45, 0), DepartureCity = "Москва", DestinationCity = "Сочи" },
            new FlightSchedule { FlightNumber = "SU-102", AirplaneType = "Superjet 100", DepartureTime = new TimeSpan(10, 15, 0), DepartureCity = "Москва", DestinationCity = "Казань" },
            new FlightSchedule { FlightNumber = "U6-301", AirplaneType = "Superjet 100", DepartureTime = new TimeSpan(14, 10, 0), DepartureCity = "Екатеринбург", DestinationCity = "Москва" }
        });

        airport.Sales.AddRange(new[] {
            new TicketSale { FlightNumber = "SU-101", SoldSeats = 150 },
            new TicketSale { FlightNumber = "SU-102", SoldSeats = 90 },
            new TicketSale { FlightNumber = "U6-301", SoldSeats = 85 }
        });

        airport.ShowFlightsGroupedByType();
        airport.ShowMaxLoadFlights();
        airport.ShowAverageLoadByType();
        airport.ShowAirplanesByDepartureCity();
        airport.ShowAirplanesByDestinationCity();
        airport.ShowFlightsByHour();
    }
}