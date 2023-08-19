
//using System.Data;
//using System.Data.SqlClient; 
//using Oracle.ManagedDataAccess.Client;

//public class DBConnectionFactory : IDBConnectionFactory
//{
//    private readonly string sqlConnectionString;
//    private readonly string oracleConnectionString; 

//    public DBConnectionFactory(string sqlConnectionString, string oracleConnectionString )
//    {
//        this.sqlConnectionString = sqlConnectionString;
//        this.oracleConnectionString = oracleConnectionString;
        
//    }

//    public IDbConnection CreateConnection(DatabaseType databaseType)
//    {
//        string connectionString = databaseType switch
//        {
//            DatabaseType.SQL => sqlConnectionString,
//            DatabaseType.Oracle => oracleConnectionString, 
//            _ => throw new ArgumentException("Invalid database type."),
//        };

//        return GetConnection(connectionString, databaseType);
//    }

//    private IDbConnection GetConnection(string connectionString, DatabaseType databaseType)
//    {
//        IDbConnection connection;
        
//        switch (databaseType)
//        {
//            case DatabaseType.SQL:
//                connection = new SqlConnection(connectionString);
//                break;
//            case DatabaseType.Oracle:
//                connection = new OracleConnection(connectionString);
//                break; 
//            default:
//                throw new ArgumentException("Invalid database type.");
//        }

//        return connection;
//    }
//}
