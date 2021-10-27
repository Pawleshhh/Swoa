using Astronomy.Units;
using CelestialObjects;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace SwoaDatabaseAPI
{
    public class SwoaSqliteDb : SwoaDb
    {

        #region Constructors

        private SwoaSqliteDb() : base()
        {

        }

        #endregion

        #region Fields

        private static SwoaSqliteDb swoaSqliteDb;

        #endregion

        #region Properties

        public static SwoaSqliteDb SwoaSqliteDbSingleton => swoaSqliteDb ??= new SwoaSqliteDb();

        #endregion

        #region Methods

        public override IEnumerable<SwoaDbRecord> GetSwoaDbRecords(string condition, SwoaDbRecordType dbRecordType)
        {
            using(var connection = new SqliteConnection($"Data Source={path}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = CreateQuery(dbRecordType, condition);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return GetSwoaDbRecord(reader, dbRecordType);
                    }

                }
            }
        }

        public override IEnumerable<SwoaDbRecord> GetSwoaDbRecordsByMagnitude(double magnitude, DbCompareOperator compareOperator, SwoaDbRecordType dbRecordType)
        {
            string condition = $"mag {GetOperator(compareOperator)} {magnitude}";

            return GetSwoaDbRecords(condition, dbRecordType);
        }

        private SwoaDbRecord GetSwoaDbRecord(SqliteDataReader reader, SwoaDbRecordType dbRecordType)
        {
            if (dbRecordType == SwoaDbRecordType.Star)
            {
                var swoaDbRecord = new SwoaDbStarRecord()
                {
                    Id = GetDataOrDefault(reader, reader.GetInt64, 0),
                    Hip = GetDataOrDefault(reader, reader.GetString, 1),
                    Hd = GetDataOrDefault(reader, reader.GetString, 2),
                    Hr = GetDataOrDefault(reader, reader.GetString, 3),
                    Gl = GetDataOrDefault(reader, reader.GetString, 4),
                    Bf = GetDataOrDefault(reader, reader.GetString, 5),
                    Proper = GetDataOrDefault(reader, reader.GetString, 6),
                    Ra = GetDataOrDefault(reader, reader.GetDouble, 7),
                    Dec = GetDataOrDefault(reader, reader.GetDouble, 8),
                    SunDist = GetDataOrDefault(reader, reader.GetDouble, 9),
                    EarthDist = GetDataOrDefault(reader, reader.GetDouble, 10),
                    Mag = GetDataOrDefault(reader, reader.GetDouble, 11),
                    AbsMag = GetDataOrDefault(reader, reader.GetDouble, 12),
                    Spect = GetDataOrDefault(reader, reader.GetString, 13),
                    Con = GetDataOrDefault(reader, reader.GetString, 14),
                };

                return swoaDbRecord;
            }

            throw new ArgumentException(nameof(dbRecordType));
        }

        private T GetDataOrDefault<T>(SqliteDataReader reader, Func<int, T> getData, int column)
        {
            if (reader.IsDBNull(column))
                return default;
            else
                return getData(column);
        }

        private string GetTableName(SwoaDbRecordType dbRecordType)
        {
            if (dbRecordType == SwoaDbRecordType.Star)
                return "stars";
            else
                throw new ArgumentException(nameof(dbRecordType));
        }

        private string CreateQuery(SwoaDbRecordType dbRecordType, string condition)
        => $"SELECT * FROM {GetTableName(dbRecordType)} WHERE {condition}";

        private string GetOperator(DbCompareOperator compareOperator)
        {
            switch(compareOperator)
            {
                case DbCompareOperator.Equal:
                    return "=";
                case DbCompareOperator.NotEqual:
                    return "<>";
                case DbCompareOperator.Greater:
                    return ">";
                case DbCompareOperator.Less:
                    return "<";
                case DbCompareOperator.GreaterEqual:
                    return ">=";
                case DbCompareOperator.LessEqual:
                    return "<=";
            }

            throw new ArgumentException(nameof(compareOperator));
        }

        #endregion

    }
}
