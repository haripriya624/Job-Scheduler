using System;
using System.Threading;

class Program
{
    static Timer timer = null!;

    static void Main(string[] args)
    {
        ScheduleHourly(38);
        // ScheduleDaily(10, 30);
        // ScheduleWeekly(DayOfWeek.Monday, 9, 0);

        Console.ReadLine();
    }

    static void ScheduleHourly(int minute)
    {
        DateTime now = DateTime.Now;
        DateTime next = new DateTime(now.Year, now.Month, now.Day, now.Hour, minute, 0);
        if (now > next)
            next = next.AddHours(1);

        TimeSpan remaining = next - now;
        if (remaining < TimeSpan.Zero)
            remaining = TimeSpan.Zero;

        timer = new Timer(_ => new HelloWorldJob().Run(), null, remaining, TimeSpan.FromHours(1));
    }

    static void ScheduleDaily(int hour, int minute)
    {
        DateTime now = DateTime.Now;
        DateTime next = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
        if (now > next)
            next = next.AddDays(1);

        TimeSpan remaining = next - now;
        if (remaining < TimeSpan.Zero)
            remaining = TimeSpan.Zero;

        timer = new Timer(_ => new HelloWorldJob().Run(), null, remaining, TimeSpan.FromDays(1));
    }

    static void ScheduleWeekly(DayOfWeek day, int hour, int minute)
    {
        DateTime now = DateTime.Now;
        int daysremaining = ((int)day - (int)now.DayOfWeek + 7) % 7;
        DateTime next = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0).AddDays(daysremaining);
        if (now > next)
            next = next.AddDays(7);

        TimeSpan remaining = next - now;
        if (remaining < TimeSpan.Zero)
            remaining = TimeSpan.Zero;

        timer = new Timer(_ => new HelloWorldJob().Run(), null, remaining, TimeSpan.FromDays(7));
    }
}  
