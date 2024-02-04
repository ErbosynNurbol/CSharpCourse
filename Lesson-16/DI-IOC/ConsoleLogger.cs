namespace Lesson_16.DI_IOC;

public class ConsoleLogger:ILogger
{
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}