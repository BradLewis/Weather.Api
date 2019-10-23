using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Data.Models;
using Weather.Data.Repositories.Interfaces;

namespace Weather.Data.Repositories
{
    public class StationRepository : IStationRepository
    {
        public Task<Station> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Station>> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
