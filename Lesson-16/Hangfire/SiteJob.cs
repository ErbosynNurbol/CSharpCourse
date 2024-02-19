using COMMON;
using Dapper;
using DBHelper;
using Hangfire;

namespace Lesson_16.Hangfire;


public class SiteJob
{
    IWebHostEnvironment _environment;
    public SiteJob(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

#region Delete Old Log Files +JobDeleteOldLogFiles()
        public void JobDeleteOldLogFiles()
        {
            string key = "jobDeleteOldLogFiles";
            if (ElordaSingleton.GetInstance.GetJobStatus(key)) return;
            ElordaSingleton.GetInstance.SetJobStatus(key, true);
            try
            {
                string logDirectoryPath = _environment.ContentRootPath + (_environment.ContentRootPath.EndsWith("/")?"":"/") + "logs";
                DirectoryInfo directory = new DirectoryInfo(logDirectoryPath);
                if (!directory.Exists) return;
                FileInfo[] txtFiles = directory.GetFiles("*.txt");
                foreach (FileInfo file in txtFiles)
                {
                    TimeSpan timeDifference = DateTime.Now - file.CreationTime;
                    if (timeDifference.Days > 7)
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "jobDeleteOldLogFiles");
            }
            finally
            {
                ElordaSingleton.GetInstance.SetJobStatus(key, false);
            }
        }
        #endregion

    public void JobRunAtMinutes()
    {
        string key = "jobRunAtMinutes"; 
        if(ElordaSingleton.GetInstance.GetJobStatus(key)) return;
          ElordaSingleton.GetInstance.SetJobStatus(key,true);
        try
        {
            string sql = string.Empty;
            Dictionary<uint, uint> articleViewDic = new Dictionary<uint, uint>();
        
            while (ElordaSingleton.GetInstance.TryDequeueViewArticleId(out uint articleId))
            {
                if (articleViewDic.ContainsKey(articleId))
                {
                    articleViewDic[articleId] += 1;
                }
                else
                {
                    articleViewDic[articleId] = 1;
                }
            }
            foreach (var item in articleViewDic)
            {
                sql += $" update article set viewCount = viewCount + {item.Value} where id = {item.Key}; ";
            }
            if (!string.IsNullOrEmpty(sql))
            {
                using (var _connection = Utilities.GetOpenConnection())
                {
                    _connection.Execute(sql);
                }
            }
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex,"Hangfirejob");
        }
        finally
        {
           ElordaSingleton.GetInstance.SetJobStatus(key,false);
        }
    }


}
