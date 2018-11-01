using System;
using System.Collections.Generic;
using App;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GeoGo.Model
{
    public class GeoData : ViewModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        [MaxLength(30)]
        public string Provider { get; set; }

        [MaxLength(30)]
        public string Description { get; set; }

        [MaxLength(40)]
        public string LastUpdate { get; set; }

        // Need to fix it to detect the coordinates datatype to set it to Point/ Polygon / Line
        [MaxLength(6)]
        public string GeometryShape { get; set; }

        [OneToMany]
        public List<Coordinate> Coordinates { get; set; }

        [OneToMany]
        public List<Property> Properties { get; set; }

        [MaxLength(20)]
        public string TypeOfImage { get; set; }



        public GeoData(string name, string type, string provider, string description)
        {
            Name = name;
            Type = type;
            Provider = provider;
            Description = description;
            LastUpdate = DateTime.Now.ToString();
            Properties = new List<Property> { };
            TypeOfImage = Types.GetIconByType(Type);
            System.Diagnostics.Debug.WriteLine(TypeOfImage);
        }

        // Only used for SQLite Can't delete
        public GeoData() { }

        // Might need to change, something bad will happens here i guess
        public void InsertCoordinate(List<Coordinate> coordinates)
        {
            Coordinates = coordinates;
            if (Coordinates.Count == 1)
                GeometryShape = "Point";
            else if (Coordinates.Count == 2)
                GeometryShape = "Line";
            else
                GeometryShape = "Polygon";
        }

        public void InsertProperty(Property prop)
        {
            Properties.Add(prop);
        }

    }
}
