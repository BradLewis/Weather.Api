using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Data.Models;

namespace Weather.Data.Repositories.Interfaces
{
    public interface IStationRepository
    {
        Task<Station> GetById(int id);

        Task<IEnumerable<Station>> GetByName(string name);
    }
}