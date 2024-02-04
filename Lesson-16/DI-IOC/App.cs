namespace Lesson_16.DI_IOC;

public class App
{
    //IOC => Inversion Of Control
    private ILogger _logger;
    public App(ILogger logger) //Constructor
    {
        _logger = logger;
    }
    public void SaveLog(string message)
    {
        _logger.ShowMessage(message);
    }
}