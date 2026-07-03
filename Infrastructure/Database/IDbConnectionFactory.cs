using System.Data.Common;

namespace Teste.ScoreAPI.Infrastructure.Database;

public interface IDbConnectionFactory
{
    DbConnection CreateConnection();
}