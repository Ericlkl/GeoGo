using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GeoGo.Model
{
    public class GeoData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength (50)]
        public string name { get; set; }

        [MaxLength(20)]
        public string type { get; set; }

        [MaxLength(30)]
        public string provider { get; set; }

        // Need to fix it to detect the coordinates datatype to set it to Point/ Polygon / Line
        [MaxLength(6)]
        public string geometryShape { get; set; }

        [OneToMany]
        public List<Coordinate> coordinates { get; set; }

        //public Dictionary<String, String> description { get; set; }


        public GeoData(string name, string type, string provider)
        {
            this.name = name;
            this.type = type;
            this.provider = provider;
        }

        // Only used for SQLite Can't delete
        public GeoData()
        {

        }

        public void InsertCoordinate(List<Coordinate> coordinates)
        {
            this.coordinates = coordinates;
            if (this.coordinates.Count == 1)
                this.geometryShape = "Point";
            else if (this.coordinates.Count == 2)
                this.geometryShape = "Line";
            else
                this.geometryShape = "Polygon";
        }

        //public void AddDescription(Dictionary<String,String> description)
        //{
        //    this.description = description;
        //}

    }
}
