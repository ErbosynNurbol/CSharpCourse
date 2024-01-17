using System.Reflection;
using Dapper;

namespace Lesson_13.Helpers;

public class ElordaResolver : SimpleCRUD.ITableNameResolver
{
    public string ResolveTableName(Type type)
    {
       return type.Name.ToLower();
    }
}