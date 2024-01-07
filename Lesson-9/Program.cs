using MODEL;
using COMMON;
using HtmlAgilityPack;
using System.Collections.Concurrent;


// for (int page = 0; page < 10; page++)
// {
//     bool v = ThreadPool.QueueUserWorkItem((stateInfo) =>
//     {
//         HtmlWeb web = new HtmlWeb();
//         var document = web.Load($"https://emle.3100.kz/kz/articles?page={page + 1}");
//         var linkList = document.DocumentNode.SelectNodes($"//a[contains(concat(' ', normalize-space(@class), ' '), ' e-list-li-name ')]");
//         foreach (var link in linkList)
//         {
//             Console.WriteLine("https://emle.3100.kz" + link.Attributes["href"].Value);
//         }
//     },page);
// }

// ConcurrentQueue<string> urlList = new ConcurrentQueue<string>();
// bool isComplate  = false;
// Task task =  Task.Run(() =>
// {
//     for (int page = 0; page < 10; page++)
//     {
//         HtmlWeb web = new HtmlWeb();
//         var document = web.Load($"https://emle.3100.kz/kz/articles?page={page + 1}");
//         var linkList = document.DocumentNode.SelectNodes($"//a[contains(concat(' ', normalize-space(@class), ' '), ' e-list-li-name ')]");
//         foreach (var link in linkList)
//         {
//             urlList.Enqueue("https://emle.3100.kz" + link.Attributes["href"].Value);
//         }
//         Task.Delay(100);
//     }
//     isComplate = true;
// });

// Task task2 =  Task.Run(() =>
// {

//     while(true)
//     {
//         while(urlList.TryDequeue(out string linkUrl)){
//                 HtmlWeb web = new HtmlWeb();
//                 var document = web.Load(linkUrl);  
//                 Console.WriteLine(document.DocumentNode.SelectSingleNode("//title")?.InnerText);  
//         }
//         Task.Delay(100);
//         if(isComplate && urlList.Count==0)
//         {
//             break;
//         }
//    }
// });

// await Task.WhenAll(task,task2);


// ConcurrentDictionary<int,string> numberList = new ConcurrentDictionary<int,string>();
// int maxNumber = 1000000;
// Task task = Task.Run(()=>{
//     for(int i = 0;i<maxNumber;i++)
//     {
//         numberList.TryAdd(i,i.ToString());
//     }
// });

// Task task2 = Task.Run(()=>{
//     for(int i = maxNumber;i<maxNumber+maxNumber;i++)
//     {
//           numberList.TryAdd(i,i.ToString());
//     }

   
// });

// await Task.WhenAll(task,task2);

// Console.WriteLine(numberList.Count());


string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string filePath = Path.Combine(desktopPath, "xcomment.txt");

Task<string> task =  File.ReadAllTextAsync(filePath);

Task<string> task2 =  File.ReadAllTextAsync(filePath);

await Task.WhenAll(task,task2);

Console.WriteLine(task.Result);



