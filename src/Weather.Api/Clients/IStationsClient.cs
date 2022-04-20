using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Clients;

public interface IStationsClient
{
	Task<Station> GetById(int id);
    Task<IEnumerable<Station>> GetByName(string name);
}

