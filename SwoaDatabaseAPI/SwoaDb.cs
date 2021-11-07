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

        protected readonly string path = @"..\..\..\..\Database\swoa.db";

        protected readonly HashSet<long> dbBlackList = new HashSet<long>();

        #endregion

        #region Methods

        public bool AddBlackListId(long id)
            => dbBlackList.Add(id);
        public bool RemoveBlackListId(long id)
            => dbBlackList.Remove(id);
        public void ClearBlackList()
            => dbBlackList.Clear();
        public bool ContainsBlackListId(long id)
            => dbBlackList.Contains(id);

        public abstract IEnumerable<SwoaDbRecord> GetAllSwoaDbRecords(string condition);
        public abstract IEnumerable<SwoaDbRecord> GetAllSwoaDbRecords(string condition, Func<bool> cancel);

        public abstract IEnumerable<SwoaDbRecord> GetSwoaDbRecords(string condition, SwoaDbRecordType dbRecordType);

        //public abstract IEnumerable<SwoaDbRecord> GetSwoaDbRecordsByMagnitude(double magnitude, DbCompareOperator compareOperator, SwoaDbRecordType dbRecordType);

        public virtual Task<IEnumerable<SwoaDbRecord>> GetAllSwoaDbRecordsAsync(string condition)
            => Task.Run(() => GetAllSwoaDbRecords(condition));

        public virtual Task<IEnumerable<SwoaDbRecord>> GetSwoaDbRecordsAsync(string condition, SwoaDbRecordType dbRecordType)
            => Task.Run(() => GetSwoaDbRecords(condition, dbRecordType));

        //public virtual Task<IEnumerable<SwoaDbRecord>> GetSwoaDbRecordsByMagnitudeAsync(double magnitude, DbCompareOperator compareOperator, SwoaDbRecordType dbRecordType)
        //    => Task.Run(() => GetSwoaDbRecordsByMagnitude(magnitude, compareOperator, dbRecordType));

        public abstract string GetTableName(SwoaDbRecordType swoaDbRecordType);

        protected abstract SwoaDbRecordFactory CreateSwoaDbRecordFactory();

        #endregion
    }
}
