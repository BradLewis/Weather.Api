using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Clients;

public class StationClient : IStationsClient
{
	public StationClient()
	{
	}

    public Task<Station> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Station>> GetByName(string name)
    {
        throw new NotImplementedException();
    }
}

