// IDBConnectionFactory.cs
using System.Data;

public interface IDBConnectionFactory
{
    IDbConnection CreateConnection(DatabaseType databaseType);
}
 
