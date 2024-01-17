using System.Collections.Concurrent;

namespace COMMON;

public class ElordaSingleton
{
    private static ElordaSingleton instance = null;
    private ConcurrentDictionary<string, string> keyValueDic = new ConcurrentDictionary<string, string>(); 
    private ElordaSingleton() { }

    public static ElordaSingleton GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new ElordaSingleton();
            }
            return instance;
        }
    }

    public void SetConnectionString(string connStr)
    {
        keyValueDic.AddOrUpdate("connectionString",connStr,(key,connStr)=>{return connStr;});
    }

    public string GetConnectionString()
    {
        if(keyValueDic.TryGetValue("connectionString",out string connStr))  return connStr;
        return "";
    }
}
