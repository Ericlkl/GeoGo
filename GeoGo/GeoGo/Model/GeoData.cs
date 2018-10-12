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
        public string Name { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        [MaxLength(30)]
        public string Provider { get; set; }

        // Need to fix it to detect the coordinates datatype to set it to Point/ Polygon / Line
        [MaxLength(6)]
        public string GeometryShape { get; set; }

        [OneToMany]
        public List<Coordinate> Coordinates { get; set; }

        //[OneToMany]
        //public List<Description> Descriptions { get; set; }


        public GeoData(string name, string type, string provider)
        {
            this.Name = name;
            this.Type = type;
            this.Provider = provider;
        }

        // Only used for SQLite Can't delete
        public GeoData()
        {

        }

        public void InsertCoordinate(List<Coordinate> coordinates)
        {
            this.Coordinates = coordinates;
            if (this.Coordinates.Count == 1)
                this.GeometryShape = "Point";
            else if (this.Coordinates.Count == 2)
                this.GeometryShape = "Line";
            else
                this.GeometryShape = "Polygon";
        }

        //public void InsertDescription(string desc_name, string desc_value){
        //    this.Descriptions.Add(new Description(desc_name ,desc_value));
        //}

    }
}
