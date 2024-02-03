namespace DBHelper;

public interface IDbConnection
{
      public  System.Data.IDbConnection GetOpenConnection();
}