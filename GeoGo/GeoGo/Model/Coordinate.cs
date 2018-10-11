using System;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;

namespace GeoGo.Model
{
    public class Coordinate
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public double latitude { get; set; }
        [NotNull]
        public double longitude { get; set; }

        [ForeignKey(typeof(GeoData))]
        public int GeoDataID { get; set; }

        public Coordinate(double lat, double lon)
        {
            this.latitude = lat;
            this.longitude = lon;
        }

        // Only used for SQLite Can't delete
        public Coordinate()
        {

        }
    }
}
