using System;
namespace GeoGo.Model
{
    public class GeoData
    {
        public string name { get; set; }
        public string type { get; set; }
        public string provider { get; set; }
        // Need to fix it to detect the coordinates datatype to set it to Point/ Polygon / Line
        public string geometryShape { get; set; }
        // Need to fix it to array which store Coordinates
        public Coordinate coordinates;


        public GeoData(string name, string type, string provider, Coordinate coordinate)
        {
            this.name = name;
            this.type = type;
            this.provider = provider;
            this.coordinates = coordinate;
        }
    }
}
