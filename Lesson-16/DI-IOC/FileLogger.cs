namespace Lesson_16.DI_IOC;

public class FileLogger:ILogger
{
    public void ShowMessage(string message)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fileName = DateTime.Now.ToString("yyyyMMdd")+".txt";
        string filePath = desktopPath+(desktopPath.EndsWith("/")?"":"/")+fileName;
        File.AppendAllText(filePath, message);
    }
}