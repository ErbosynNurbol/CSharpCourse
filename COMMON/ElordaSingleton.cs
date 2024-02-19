using System.Collections.Concurrent;

namespace COMMON;

public class ElordaSingleton
{
    private static ElordaSingleton instance = null;
    private ConcurrentDictionary<string, string> keyValueDic = new ConcurrentDictionary<string, string>();
    private ConcurrentQueue<uint> queueArticleId = new ConcurrentQueue<uint>();
    private ConcurrentDictionary<string, bool> jobStatusDic = new ConcurrentDictionary<string, bool>();
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
        keyValueDic.AddOrUpdate("connectionString", connStr, (key, connStr) => { return connStr; });
    }

    public string GetConnectionString()
    {
        if (keyValueDic.TryGetValue("connectionString", out string connStr)) return connStr;
        return "";
    }

    public void EnqueueViewArticleId(uint articleId)
    {
        queueArticleId.Enqueue(articleId);
    }

    public bool TryDequeueViewArticleId(out uint articleId)
    {
        return queueArticleId.TryDequeue(out articleId);
    }

    public void SetJobStatus(string jobName, bool status)
    {
        if (jobStatusDic.ContainsKey(jobName))
        {
            if (!status)
            {
                jobStatusDic.Remove(jobName, out status);
            }
            else
            {
                jobStatusDic[jobName] = status;
            }
        }
        else
        {
            if (status)
            {
                jobStatusDic.TryAdd(jobName, status);
            }
        }
    }

    public bool GetJobStatus(string jobName)
    {
        if (jobStatusDic.ContainsKey(jobName))
            return jobStatusDic[jobName];
        return false;
    }
}
