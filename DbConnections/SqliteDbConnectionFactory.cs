using System.Data;
using Microsoft.Data.Sqlite;

namespace CardApi.DbConnections;

public class SqliteDbConnectionFactory(string? connectionString)
{
    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(connectionString);
    }
}