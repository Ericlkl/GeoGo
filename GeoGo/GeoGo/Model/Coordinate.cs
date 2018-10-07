using System;
namespace GeoGo.Model
{
    public class Coordinate
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Coordinate(double lat, double lon)
        {
            this.latitude = lat;
            this.longitude = lon;
        }
    }
}
