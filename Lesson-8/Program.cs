using System.Web;
using COMMON;
using HtmlAgilityPack;
using MODEL;
using Newtonsoft.Json;

// string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
// List<string> files  = FileHelper.GetAllFiles(desktopPath);
// foreach (string item in files)
// {
//     Console.WriteLine(item);
// }


// string phoneNumber = "97003142857";
// string phone = string.Empty;
// if(!RegexHelper.IsPhone(phoneNumber,out phone))
// {
//     Console.WriteLine("Please enter a valid phone number!");
//     return;
// }


// Person person = new Person
// {
//     Name = "John Doe",
//     Age = 30,
//     IsAlive = true
// };

// // Serialize the Person object to a JSON string
// string jsonString = JsonHelper.SerializeObject(person);
// Console.WriteLine("Serialized JSON: " + jsonString);

// // Deserialize the JSON string back to a Person object
// Person deserializedPerson = JsonHelper.DeserializeObject<Person>(jsonString);
// Console.WriteLine("Deserialized Person Name: " + deserializedPerson.Name);



// HtmlWeb web = new HtmlWeb();
// HtmlDocument document = web.Load("https://mig.kz");
// HtmlNodeCollection htmlNodeList = document.DocumentNode.SelectNodes($"//td[contains(concat(' ', normalize-space(@class), ' '), ' currency ')]");
// foreach (HtmlNode htmlNode in htmlNodeList)
// {
//               HtmlNode sibling = htmlNode.NextSibling;
//                     while (sibling != null)
//                     {
//                         if (sibling.NodeType == HtmlNodeType.Element)
//                         {
//                             break;
//                         }
//                         sibling = sibling.NextSibling;
//                     }

//             Console.WriteLine($"{HttpUtility.HtmlDecode(htmlNode.InnerText)} = {sibling.InnerHtml}");
    
// }


// int pageCount = 11;


// for(int page = 0 ;page < pageCount;page++){
//     HtmlWeb web = new HtmlWeb();
//     var document  = web.Load($"https://emle.3100.kz/kz/articles?page={page+1}");

//     var linkList = document.DocumentNode.SelectNodes($"//a[contains(concat(' ', normalize-space(@class), ' '), ' e-list-li-name ')]");
//     foreach(var link in linkList)
//     {
//         Console.WriteLine("https://emle.3100.kz"+link.Attributes["href"].Value);
//     }

// }   





  string url = "https://www.qazlatyn.kz/api/qazlatyn/getarticlelist";

        using (var client = new HttpClient())
        {
            // Form data
            var formData = new Dictionary<string, string>
            {
                { "page", "2" },
                { "pageSize", "10" }
            };

            // Encode form data
            var content = new FormUrlEncodedContent(formData);

            // Send a POST request
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // Read response content (if needed)
                string responseContent = await response.Content.ReadAsStringAsync();
                AjaxModel ajaxModel  = JsonHelper.DeserializeObject<AjaxModel>(responseContent);
                if(ajaxModel!=null && ajaxModel.Status.Equals("Success",StringComparison.OrdinalIgnoreCase))
                {
                     PagedDataModel pagedDataModel  = JsonHelper.DeserializeObject<PagedDataModel>(ajaxModel.Data.ToString());
                     if(pagedDataModel!=null){

                          List<ArticleModel> articleList =   JsonHelper.DeserializeObject<List<ArticleModel>>(pagedDataModel.DataList.ToString());
                           foreach(ArticleModel articleModel in articleList)
                           {
                              Console.WriteLine(articleModel.ViewCount);
                           }
                     }
                   
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }