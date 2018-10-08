using System;
using System.Collections.Generic;

namespace GeoGo.Model
{
    public class GeoData
    {
        public string name { get; set; }
        public string type { get; set; }
        public string provider { get; set; }
        public List<Coordinate> coordinates;
        public Dictionary<String, String> description;

        // Need to fix it to detect the coordinates datatype to set it to Point/ Polygon / Line
        public string geometryShape;

        public GeoData(string name, string type, string provider, List<Coordinate> coordinates)
        {
            this.name = name;
            this.type = type;
            this.provider = provider;
            this.coordinates = coordinates;

            if (this.coordinates.Count == 1)
                this.geometryShape = "Point";
            else if (this.coordinates.Count == 2)
                this.geometryShape = "Line";
            else
                this.geometryShape = "Polygon";
        }

        public void AddDescription(Dictionary<String,String> description)
        {
            this.description = description;
        }

    }
}
