using Astronomy;
using Astronomy.Units;
using CelestialObjects;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;

namespace SwoaDatabaseAPI
{
    public class SwoaSqliteDb : SwoaDb
    {

        #region Constructors

        private SwoaSqliteDb() : base()
        {
            CreateSwoaDbRecordFactory();
        }

        #endregion

        #region Fields

        private static SwoaSqliteDb swoaSqliteDb;

        private SwoaSqliteDbRecordFactory factory;

        #endregion

        #region Properties

        public static SwoaSqliteDb SwoaSqliteDbSingleton => swoaSqliteDb ??= new SwoaSqliteDb();

        #endregion

        #region Methods

        public override IEnumerable<SwoaDbRecord> GetAllSwoaDbRecords(string condition)
        {
            return GetAllSwoaDbRecords(condition);
        }

        public override IEnumerable<SwoaDbRecord> GetAllSwoaDbRecords(string condition, CancellationToken ct = default)
        {
            using (var connection = GetSqliteConnection())
            {
                IEnumerable<SwoaDbRecord> result = Enumerable.Empty<SwoaDbRecord>();
                foreach (SwoaDbRecordType dbRecordType in Enum.GetValues(typeof(SwoaDbRecordType)))
                {
                    result = result.Concat(GetDbRecordsByType(connection, condition, dbRecordType, ct));

                    ct.ThrowIfCancellationRequested();
                }

                return result;
            }
        }

        public override IEnumerable<SwoaDbRecord> GetSwoaDbRecords(string condition, SwoaDbRecordType dbRecordType)
        {
            using(var connection = GetSqliteConnection())
            {
                var result = GetDbRecordsByType(connection, condition, dbRecordType);

                return result;
            }
        }

        //public override IEnumerable<SwoaDbRecord> GetSwoaDbRecordsByMagnitude(double magnitude, DbCompareOperator compareOperator, SwoaDbRecordType dbRecordType)
        //{
        //    string condition = $"mag {GetOperator(compareOperator)} {magnitude}";

        //    return GetSwoaDbRecords(condition, dbRecordType);
        //}

        public override string GetTableName(SwoaDbRecordType dbRecordType)
        {
            if (dbRecordType == SwoaDbRecordType.Star)
                return "stars";
            else
                throw new ArgumentException(nameof(dbRecordType));
        }

        protected IEnumerable<SwoaDbRecord> GetDbRecordsByType(SqliteConnection connection, string condition, SwoaDbRecordType dbRecordType, CancellationToken ct = default)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = CreateQuery(dbRecordType, condition);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return factory.CreateSwoaDbRecord(reader, dbRecordType);

                    ct.ThrowIfCancellationRequested();
                }
            }
        }

        protected override SwoaDbRecordFactory CreateSwoaDbRecordFactory()
        {
            return factory ??= new SwoaSqliteDbRecordFactory();
        }

        private string CreateQuery(SwoaDbRecordType dbRecordType, string condition)
        => $"SELECT * FROM {GetTableName(dbRecordType)} WHERE {condition}";

        private SqliteConnection GetSqliteConnection()
        {
            var connection = new SqliteConnection($"Data Source={path}");

            connection.CreateFunction("skycontains",
                (double ra, double dec, string date, double latitude, double longitude) =>
                {
                    var dateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", null);

                    var result = CoordinatesConverter.EquatorialToHorizonCoords(ra / 24.0 * 360.0, dec, dateTime.ToUniversalTime(), latitude, longitude);

                    return result.alt > 0.0;
                });
            connection.CreateFunction("notblacklisted",
                (long id) => !dbBlackList.Contains(id));

            return connection;
        }


        //private string GetOperator(DbCompareOperator compareOperator)
        //{
        //    switch(compareOperator)
        //    {
        //        case DbCompareOperator.Equal:
        //            return "=";
        //        case DbCompareOperator.NotEqual:
        //            return "<>";
        //        case DbCompareOperator.Greater:
        //            return ">";
        //        case DbCompareOperator.Less:
        //            return "<";
        //        case DbCompareOperator.GreaterEqual:
        //            return ">=";
        //        case DbCompareOperator.LessEqual:
        //            return "<=";
        //    }

        //    throw new ArgumentException(nameof(compareOperator));
        //}

        #endregion

    }
}
