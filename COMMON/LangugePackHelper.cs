using System.Net.Http.Headers;
using System.Reflection;

namespace COMMON;

public class LangugePackHelper()
{
    public static string GetLanguagePackJsonString()
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://www.sozdikqor.org");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/api/query/all");
            HttpResponseMessage response = client.SendAsync(request).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
    public static Dictionary<string,Dictionary<string ,string>> LangugePack()
    {
        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        string directoryPath = Path.GetDirectoryName(assemblyPath);
        string filePath = directoryPath+"/languagepack.txt";
        try{
           string languagePackStr = GetLanguagePackJsonString();
           File.WriteAllText(filePath,languagePackStr);
           return JsonHelper.DeserializeObject<Dictionary<string,Dictionary<string ,string>>>(languagePackStr);
        }catch(Exception ex)
        {
           string languagePackStr =File.Exists(filePath)? File.ReadAllText(filePath):"";
           if(!string.IsNullOrEmpty(languagePackStr)){
                    return JsonHelper.DeserializeObject<Dictionary<string,Dictionary<string ,string>>>(languagePackStr);
           }
            return null;
        }
       
    }
}