using System;
using System.Collections.Generic;

public class Airplane
{
    public DateTime ScheduledTime { get; set; }
    public DateTime ActualTime { get; set; }
    public bool IsTakeoff { get; set; }

    public Airplane(DateTime scheduledTime, bool isTakeoff)
    {
        ScheduledTime = scheduledTime;
        IsTakeoff = isTakeoff;
    }
}

class Airport
{
    private Queue<Airplane> takeoff = new Queue<Airplane>();
    private Queue<Airplane> landing = new Queue<Airplane>();

    private DateTime firstTime;
    private DateTime lastTime;

    public void AddTakeoff(Airplane plane)
    {
        takeoff.Enqueue(plane);
    }

    public void AddLanding(Airplane plane)
    {
        landing.Enqueue(plane);
    }

    public TimeSpan GetTotalServiceTime()
    {
        return lastTime - firstTime;
    }

    public Queue<Airplane> ProcessMainQueue()
    {
        Queue<Airplane> output = new Queue<Airplane>();
        DateTime lastProcessedTime = DateTime.MinValue;
        bool isFirstPlane = true;

        while (takeoff.Count > 0 || landing.Count > 0)
        {
            Airplane nextPlane = null;

            if (takeoff.Count > 0 && landing.Count > 0)
            {
                Airplane tPlane = takeoff.Peek();
                Airplane lPlane = landing.Peek();

                if (tPlane.ScheduledTime <= lPlane.ScheduledTime)
                {
                    if (lPlane.ScheduledTime<= tPlane.ScheduledTime.AddMinutes(5))
                    {
                        landing.Dequeue();
                    }
                    nextPlane = takeoff.Dequeue();
                }
                else
                {
                    nextPlane = landing.Dequeue();
                }
            }
            else if (takeoff.Count > 0)
            {
                nextPlane = takeoff.Dequeue();
            }
            else
            {
                nextPlane = landing.Dequeue();
            }

            DateTime actualTime = nextPlane.ScheduledTime;

            if (!isFirstPlane && actualTime < lastProcessedTime.AddMinutes(5))
            {
                actualTime = lastProcessedTime.AddMinutes(5);
            }

            nextPlane.ActualTime = actualTime;
            output.Enqueue(nextPlane);
            lastProcessedTime = actualTime;

            lastTime = actualTime;
            if (isFirstPlane)
            {
                firstTime = actualTime;
                isFirstPlane = false;
            }
        }

        return output;
    }

    public void PrintResults(Queue<Airplane> processedQueue)
    {
        Console.WriteLine("\n--- Итоговое расписание полосы ---");
        int count = 1;
        foreach (var plane in processedQueue)
        {
            string type = plane.IsTakeoff ? "Взлет  " : "Посадка";
            Console.WriteLine($"{count}. {type}: {plane.ActualTime}");
            count++;
        }
        Console.WriteLine($"Полное время обслуживания: {GetTotalServiceTime().TotalMinutes} минут(ы).");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Airport airport = new Airport();
        DateTime today = DateTime.Today;

        Console.Write("Введите количество самолетов на взлет: ");
        int takeoffCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < takeoffCount; i++)
        {
            Console.Write($"Введите время взлета для самолета: ");
            TimeSpan time = TimeSpan.Parse(Console.ReadLine());
            airport.AddTakeoff(new Airplane(today.Add(time), true));
        }

        Console.Write("Введите количество самолетов на посадку: ");
        int landingCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < landingCount; i++)
        {
            Console.Write($"Введите время посадки для самолета: ");
            TimeSpan time = TimeSpan.Parse(Console.ReadLine());
            airport.AddLanding(new Airplane(today.Add(time), false));
        }

        Queue<Airplane> finalQueue = airport.ProcessMainQueue();

        airport.PrintResults(finalQueue);
    }
}