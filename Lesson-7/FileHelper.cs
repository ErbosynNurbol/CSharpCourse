
namespace Lesson_7
{
    public class FileHelper
    {
        static public List<string> GetAllFiles(string directoryPath)
        {
            List<string> filePathList = new List<string>();
            if(!Directory.Exists(directoryPath)) return filePathList;
         filePathList.AddRange(Directory.GetFiles(directoryPath).ToList());

          foreach(string dPath  in Directory.GetDirectories(directoryPath))
           {
                List<string> itemFilePathList = GetAllFiles(dPath);
                filePathList.AddRange(itemFilePathList);
           }
             return filePathList;
        }

    }
}