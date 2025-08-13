using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.Repositories
{
    public interface ISQLiteManager
    {
        ISQLiteAsyncConnection Connection { get; }

        Task CreateTables();
    }
}
