
//c://Users/james/Documents/Files/1.txt
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text;

string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
desktopPath = Path.Combine(desktopPath,"Files");
string filePath = Path.Combine(desktopPath, "hello.txt");
string directoryPath = Path.GetDirectoryName(filePath);


List<string> files  = COMMON.FileHelper.GetAllFiles(directoryPath);
foreach (string item in files)
{
    Console.WriteLine(item);
}







// if(!Directory.Exists(directoryPath)){
//     Directory.CreateDirectory(directoryPath);
// }
// if(!File.Exists(filePath)){
//    using(FileStream fs =  File.Create(filePath))
//    {
//            byte[] info = new UTF8Encoding(true).GetBytes("Hello");
//                 // Write the contents to the file
//            fs.Write(info, 0, info.Length);
//    }

//     using(FileStream fs =   File.OpenWrite(filePath))
//    {
//            byte[] info = new UTF8Encoding(true).GetBytes("Hello");
//            FileInfo fileInfo = new FileInfo(filePath);
//            fs.Write(info, 2, 3);
//    }
// }

// File.AppendAllText(filePath, $"{Environment.NewLine}Hello");

// List<string> list = new List<string>();
// list.Add("World!");
// list.Add("HHHHH!");
// File.AppendAllLines(filePath,list);
// string fileFormat = Path.GetExtension(filePath);
// string fileName = Path.GetFileNameWithoutExtension(filePath);
// string descFilePath  = Path.Combine(directoryPath,$"{fileName}22{fileFormat}");
// File.Copy(filePath,descFilePath);






// DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
// if(!directoryInfo.Exists){
//     directoryInfo.Create();
// }
// FileInfo fileInfo = new FileInfo(filePath);
// if(!fileInfo.Exists){
//     fileInfo.Create();
// }


// try
// {
//     long fileLength = fileInfo.Length;
//     if (fileLength > 1024 * 1024)
//     {
//             using (FileStream fs = File.Open(filePath,FileMode.OpenOrCreate))
//             {
//                 byte[] b = new byte[1024*1024]; 
//                 UTF8Encoding temp = new UTF8Encoding(true);

//                 while (fs.Read(b, 0, b.Length) > 0)
//                 {
//                     Console.WriteLine(temp.GetString(b));
//                 }
//             }


//         // foreach (string line in File.ReadLines(filePath))
//         // {
//         //     Console.WriteLine(line); // Process each line as needed
//         // }
//     }
//     else
//     {
//         string text = File.ReadAllText(filePath);
//         Console.WriteLine(text);
//     }
// }
// catch (Exception ex)
// {

// }




// // File.WriteAllText(filePath,"Hahahahahahahahahahahahahah");

// // File.WriteAllText(filePath,"hehe");



