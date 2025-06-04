using Quartz;
using System;
using System.IO;
using System.Threading.Tasks;

public class HelloWorldJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        string message = $"[{DateTime.Now}] Hello World from Job ID: {context.JobDetail.Key}\n";
        Console.WriteLine(message);
        File.AppendAllText("job_log.txt", message); // Log to a file
        return Task.CompletedTask;
    }
}
