using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GeoGo.Model
{
    public class Property
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(20),NotNull]
        public string PropertyName { get; set; }

        [MaxLength(50), NotNull]
        public string PropertyValue { get; set; }

        [ForeignKey(typeof(GeoData))]
        public int GeoDataID { get; set; }

        public Property(String propertyName, String propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }

        public Property(){}
    }
}
