using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GeoGo.Model
{
    public class Description
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(20),NotNull]
        public string DescriptionName { get; set; }
        [MaxLength(50), NotNull]
        public string DescriptionValue { get; set; }

        [ForeignKey(typeof(GeoData))]
        public int GeoDataID { get; set; }

        public Description(String descritionName, String descriptionValue)
        {
            this.DescriptionName = descritionName;
            this.DescriptionValue = descriptionValue;
        }
    }
}
