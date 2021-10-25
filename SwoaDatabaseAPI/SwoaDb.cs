using CelestialObjects;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwoaDatabaseAPI
{
    public abstract class SwoaDb
    {

        #region Constructors

        public SwoaDb()
        {

        }

        #endregion

        #region Fields

        protected readonly string path = @"N:\Programowanie\Ogólne\Swoa\Database\swoa.db";

        #endregion

        #region Methods

        public abstract IEnumerable<CelestialObject> GetCelestialObjects(string query);

        public virtual Task<IEnumerable<CelestialObject>> GetCelestialObjectsAsync(string query)
            => Task.Run(() => GetCelestialObjects(query));

        #endregion
    }
}
