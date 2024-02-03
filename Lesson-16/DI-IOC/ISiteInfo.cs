using Org.BouncyCastle.Crypto.Agreement.Srp;

namespace Lesson_16.DI_IOC;

public interface ISiteInfo 
{
    public string Connection{get;set;}
    public string SiteUrl{get;set;}
    public int Port{get;set;}
    public DateTime SaveTime{get;set;}
}