using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Weather.Data
{
    public interface IDatabaseService
    {
        IDbConnection GetConnection();
    }
}
