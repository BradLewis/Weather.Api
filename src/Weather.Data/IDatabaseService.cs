using System.Data;

namespace Weather.Data
{
    public interface IDatabaseService
    {
        IDbConnection GetConnection();
    }
}