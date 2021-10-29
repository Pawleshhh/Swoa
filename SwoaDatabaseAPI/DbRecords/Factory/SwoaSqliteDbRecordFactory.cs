using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwoaDatabaseAPI
{
    public class SwoaSqliteDbRecordFactory : SwoaDbRecordFactory
    {
        public override SwoaDbRecord CreateSwoaDbRecord(DbDataReader reader, SwoaDbRecordType swoaDbRecordType)
        {
            switch (swoaDbRecordType)
            {
                case SwoaDbRecordType.Star:
                    return CreateSwoaDbStarRecord(reader);
            }

            throw new ArgumentException(nameof(swoaDbRecordType));
        }

        public SwoaDbStarRecord CreateSwoaDbStarRecord(DbDataReader reader)
        {
            return new SwoaDbStarRecord()
            {
                Id = SwoaDbHelper.GetDataOrDefault(reader, reader.GetInt64, 0),
                Hip = SwoaDbHelper.GetDataOrDefault(reader, reader.GetString, 1),
                Hd = SwoaDbHelper.GetDataOrDefault(reader, reader.GetString, 2),
                Hr = SwoaDbHelper.GetDataOrDefault(reader, reader.GetString, 3),
                Gl = SwoaDbHelper.GetDataOrDefault(reader, reader.GetString, 4),
                Bf = SwoaDbHelper.GetDataOrDefault(reader, reader.GetString, 5),
                Proper = SwoaDbHelper.GetDataOrDefault(reader, reader.GetString, 6),
                Ra = SwoaDbHelper.GetDataOrDefault(reader, reader.GetDouble, 7),
                Dec = SwoaDbHelper.GetDataOrDefault(reader, reader.GetDouble, 8),
                SunDist = SwoaDbHelper.GetDataOrDefault(reader, reader.GetDouble, 9),
                EarthDist = SwoaDbHelper.GetDataOrDefault(reader, reader.GetDouble, 10),
                Mag = SwoaDbHelper.GetDataOrDefault(reader, reader.GetDouble, 11),
                AbsMag = SwoaDbHelper.GetDataOrDefault(reader, reader.GetDouble, 12),
                Spect = SwoaDbHelper.GetDataOrDefault(reader, reader.GetString, 13),
                Con = SwoaDbHelper.GetDataOrDefault(reader, reader.GetString, 14),
            };
        }

    }
}
