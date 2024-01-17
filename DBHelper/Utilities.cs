using COMMON;

namespace DBHelper;

public class Utilities
{
        public static System.Data.IDbConnection GetOpenConnection()
        {          
            string connectionString = ElordaSingleton.GetInstance.GetConnectionString();
            System.Data.IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            connection.Open();
            return connection; 
        }
}
