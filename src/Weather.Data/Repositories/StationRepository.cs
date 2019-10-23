using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Data.Models;
using Weather.Data.Repositories.Interfaces;

namespace Weather.Data.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly IDatabaseService _databaseService;
        
        public StationRepository( IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Station> GetById(int id)
        {
            using (var connection = _databaseService.GetConnection())
            {
                var query = "SELECT TOP 1 * FROM Stations WHERE Id = @Id";
                var result = await connection.QueryAsync<Station>(query, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public Task<IEnumerable<Station>> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
