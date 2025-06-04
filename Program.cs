using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        //Creating and starting scheduler
        StdSchedulerFactory factory = new StdSchedulerFactory();
        IScheduler scheduler = await factory.GetScheduler();
        await scheduler.Start();

        //Hourly job:runs every hour at minute "30"
        IJobDetail hourlyJob = JobBuilder.Create<HelloWorldJob>()
            .WithIdentity("hourlyJob", "group1")
            .Build();

        ITrigger hourlyTrigger = TriggerBuilder.Create()
            .WithIdentity("hourlyTrigger", "group1")
            .WithSchedule(CronScheduleBuilder.CronSchedule("0 30 * ? * *")) // minute 30 every hour
            .Build();

        //Daily job: runs daily at 15:52 (tested for this time)
        IJobDetail dailyJob = JobBuilder.Create<HelloWorldJob>()
            .WithIdentity("dailyJob", "group1")
            .Build();

        ITrigger dailyTrigger = TriggerBuilder.Create()
            .WithIdentity("dailyTrigger", "group1")
            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(15, 52))
            .Build();

        //Weekly job: runs on every Monday at 9:00 AM
        IJobDetail weeklyJob = JobBuilder.Create<HelloWorldJob>()
            .WithIdentity("weeklyJob", "group1")
            .Build();

        ITrigger weeklyTrigger = TriggerBuilder.Create()
            .WithIdentity("weeklyTrigger", "group1")
            .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Monday, 9, 0))
            .Build();

        // Scheduling the jobs
        await scheduler.ScheduleJob(hourlyJob, hourlyTrigger);
        await scheduler.ScheduleJob(dailyJob, dailyTrigger);
        await scheduler.ScheduleJob(weeklyJob, weeklyTrigger);

        Console.WriteLine("Scheduler started. Press Enter to stop...");
        Console.ReadLine();

        await scheduler.Shutdown();
    }
}
