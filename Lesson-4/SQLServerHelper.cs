

public class SQLServerHelper : IDataHelper
{
    public string Name => throw new NotImplementedException();

    public SQLServerHelper()
    {
        //Connect to database
    }

    public List<string> GetList(int id)
    {
        
        return new List<string>();
    }

    public List<int> GetIdList()
    {
        //
        return new List<int>();
    }

     public int Exqute(string sql)
     {
        //
        return 1;
     }


}