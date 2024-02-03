

namespace Lesson_16.DI_IOC;

public class SiteInfo :ISiteInfo
{
    public string Connection{get;set;}
    public string SiteUrl{get;set;}
    public int Port{get;set;}
     public DateTime SaveTime{get;set;}
}