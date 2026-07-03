using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Teste.ScoreAPI.Infrastructure.Database;

public sealed class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Connection string é obrigatória.", nameof(connectionString));

        _connectionString = connectionString;
    }

    public DbConnection CreateConnection() => new SqlConnection(_connectionString);
}