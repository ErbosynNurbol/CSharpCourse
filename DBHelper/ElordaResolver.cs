using Dapper;

namespace DBHelper;
public class ElordaResolver : SimpleCRUD.ITableNameResolver
{
    public string ResolveTableName(Type type)
    {
       return type.Name.ToLower();
    }
}