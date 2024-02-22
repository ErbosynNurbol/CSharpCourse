using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using COMMON;
using Dapper;
using DBHelper;
using Microsoft.Extensions.Caching.Memory;
using MODEL;

namespace Lesson_16.Cache;

public class ElCache
{

    private static Dictionary<string,Dictionary<string,string>> GetLanguagePack(IMemoryCache _memoryCache)
    {
           string key = "languagePack";
            Dictionary<string,Dictionary<string,string>> languagePack = null;
           if(!_memoryCache.TryGetValue<Dictionary<string,Dictionary<string,string>>>(key,out languagePack))
           {
               languagePack  = LangugePackHelper.LangugePack();
               _memoryCache.Set<Dictionary<string,Dictionary<string,string>>>(key,languagePack,DateTimeOffset.MaxValue);
           }
          return languagePack;
    }

    public void ClearLanguagePackCache(IMemoryCache _memoryCache)
    {
        _memoryCache.Remove("languagePack");
    }

    public static string GetLanguageValue(IMemoryCache _memoryCache, string localKey,string language) 
    {
          var dic =  ElCache.GetLanguagePack(_memoryCache);
           switch(language)
           {
            case "latyn":{
                 if(dic.ContainsKey(localKey) && dic[localKey].ContainsKey("kz")){
                    return ConvertHelper.Cyrl2Latyn(dic[localKey]["kz"]);
                 }
            }break;
               case "tote":{
                  if(dic.ContainsKey(localKey) && dic[localKey].ContainsKey("kz")){
                    return Cyrl2ToteHelper.Cyrl2Tote(dic[localKey]["kz"]);
                 }

               }break;
               default:{
                    if(dic.ContainsKey(localKey) && dic[localKey].ContainsKey(language))
                            return dic[localKey][language];
               } break;
           }
            
             return localKey;
    }
    

    #region Get Latest Article List +GetLatestArticleList(IMemoryCache _memoryCache,int takeCount)
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
    #endregion

    public static void ClearLatestArticleListCache(IMemoryCache _memoryCache)
    {
        for(int i = 1;i<=25;i++){
            _memoryCache.Remove($"latestArticleList_{i}");
        }
    }   

}