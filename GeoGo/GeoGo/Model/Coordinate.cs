using System;
using App;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GeoGo.Model
{
    public class Coordinate : ViewModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public double Latitude { get; set; }
        [NotNull]
        public double Longitude { get; set; }

        [ForeignKey(typeof(GeoData))]
        public int GeoDataID { get; set; }

        public Coordinate(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        // Only used for SQLite Can't delete
        public Coordinate() { }
    }
}
