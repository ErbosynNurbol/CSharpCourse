using Dapper;
using DBHelper;
using Microsoft.Extensions.Caching.Memory;
using MODEL;

namespace Lesson_16.Cache;

public class ElCache
{
    public static List<Article> GetLatestArticleList(IMemoryCache _memoryCache,int takeCount)
    {
            string key = $"latestArticleList_{takeCount}";
            List<Article> latestArticleList;
            if(!_memoryCache.TryGetValue<List<Article>>(key, out latestArticleList))
            {
                using(var _connection = Utilities.GetOpenConnection())
                {
                    string querySql = "where qStatus  = 0 ";
                    object queryObj = new {takeCount};
                    latestArticleList =  _connection.Query<Article>("select title,thumbnailUrl,latynUrl,addTime,author,shortDescription from article "+querySql + " order by addTime desc limit @takeCount ",queryObj)
                   .Select(x=>new Article(){
                     Title = x.Title,
                     ThumbnailUrl =  string.IsNullOrEmpty(x.ThumbnailUrl)?QarBaseController.no_image:"https://infohub.kz"+x.ThumbnailUrl,
                     LatynUrl = x.LatynUrl,
                     AddTime = x.AddTime,
                     Author = x.Author,
                     ShortDescription = x.ShortDescription
                   }).ToList();
                }
               _memoryCache.Set<List<Article>>(key,latestArticleList,DateTimeOffset.Now.AddMinutes(1));
            }
           return latestArticleList;
    }

    public static void ClearLatestArticleListCache(IMemoryCache _memoryCache)
    {
        for(int i = 1;i<=25;i++){
            _memoryCache.Remove($"latestArticleList_{i}");
        }
    }   

}