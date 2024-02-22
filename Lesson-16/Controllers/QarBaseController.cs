using System.Data;
using COMMON;
using Dapper;
using Lesson_16.Cache;
using Microsoft.Extensions.Caching.Memory;
using MODEL;

namespace Lesson_16;

public class QarBaseController : Controller
{ 

    public  static readonly string no_image = "/img/no_image.png";
    IMemoryCache _memoryCache;
     public QarBaseController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

       #region  Қолданушының IP әдіресін алу +GetIPAddress()
          public string GetIPAddress()
        {
            string locationIP = HttpContext.Connection.RemoteIpAddress.ToString();
            if (HttpContext.Request.Headers["X-Real-IP"].Count() > 0)
            {
                locationIP = HttpContext.Request.Headers["X-Real-IP"];
            }
            return locationIP;
        }
        #endregion

        public uint GetPersonId()
        {
            return HttpContext.User.Identity.PersonId();
        }

     public int GetIntQueryParam(string paramName, int defaultValue = 0)
     {
         return int.TryParse(Request.Query[paramName].ToString(), out int paramValue)?paramValue:defaultValue;
     }

     public string GetStringQueryParam(string paramName)
     {
         return Request.Query[paramName].ToString();
     }

    public  string T(string localKey)
    {
         string language =  (ViewData["language"]??string.Empty) as string;
         return ElCache.GetLanguageValue(_memoryCache,localKey,language);
    }

    #region Save Mutli Language +SaveMutliLanguage(IDbConnection _connection, List<Multilanguage> itemList, string tableName, uint columnId)
    public void SaveMutliLanguage(IDbConnection _connection, List<Multilanguage> itemList, string tableName, uint columnId)
    {
        if(itemList == null || itemList.Count() == 0 || string.IsNullOrEmpty(tableName)||columnId<=0)
            throw new Exception("SaveMutliLanguage Param error");

         foreach(Multilanguage item in itemList)
         {  
                Multilanguage  multilanguage =  _connection.GetList<Multilanguage>("where qStatus in (0,1) and language = @language and tableName = @tableName and columnName = @columnName and  columnId = @columnId",
                        new {language =item.Language, tableName,columnName = item.ColumnName ,columnId}).FirstOrDefault();
                if(multilanguage==null){
                    if(string.IsNullOrWhiteSpace(item.ColumnValue)) continue;
                    _connection.Insert<Multilanguage>(new Multilanguage(){
                      ColumnId = columnId,
                      TableName = tableName,
                      ColumnName = item.ColumnName,
                      ColumnValue = item.ColumnValue,
                      Language = item.Language,
                      QStatus = 0
                    });
                }else{
                    if(string.IsNullOrWhiteSpace(item.ColumnValue)){
                            multilanguage.QStatus = 1;
                    }else{
                        multilanguage.ColumnValue = item.ColumnValue;
                        multilanguage.QStatus = 0;
                    }
                    _connection.Update<Multilanguage>(multilanguage);
                }
         }
    }
    #endregion

    public List<Multilanguage> GetMultiLanguageList(IDbConnection _connection, string tableName,List<uint> columnIdList, string language = null, List<string> columnNameList = null)
    {
             if(string.IsNullOrEmpty(tableName)||columnIdList == null||columnIdList.Count()<=0)
                  return new  List<Multilanguage>();

            string querySql = $" where qStatus = 0 and tableName = @tableName  and columnId in ({string.Join(",",columnIdList)})";
            if(!string.IsNullOrEmpty(language)){
                querySql += " and language = @language ";
            }
            object queryObj = new {tableName, language};
            if(columnNameList!=null && columnNameList.Count()>0){
                querySql += $" and columnName in  ({string.Join(",",columnNameList.Select(x=>"'"+x+"'").ToArray())})";
            }
            
         return   _connection.GetList<Multilanguage>(querySql,queryObj).ToList();

    }
}