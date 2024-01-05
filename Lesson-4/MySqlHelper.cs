
using System.Net.NetworkInformation;

public class MySqlHelper:IDataHelper,IMyHelper
{

      public string Name { 
         get {
               
                return "";
             }  
        }

    public MySqlHelper()
    {
        //Connect to database
    }

    public List<string> GetList(int id)
    {

        return new List<string>();
    }

     public List<int> GetIdList()
    {
        return new List<int>();
    }

     public int Exqute(string sql)
     {
        //
        return 1;
     }

    public void GetMyFriendList()
    {
      
    }
}