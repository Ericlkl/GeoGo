using System;
using System.Collections.Generic;
using System.Text;

namespace GeoGo.Model
{
    public static class Types
    {
        public static Dictionary<string, string> intToType = new Dictionary<string, string>
        {
            {"LED","ic_green.png" },
            { "Tree","ic_tree.png" },
            { "Wifi","ic_wifi.png" },
            { "Solar","ic_yellow.png" },
            { "Halogan","ic_blue.png" },
            { "Vehicle Access","ic_car.png" },
            { "Toliet","ic_toliet.png" },

        };

        public static String GetIconByType(String type)
        {
            if (intToType.ContainsKey(type))
            {
                return intToType[type];
            }
            else
            {
                return "";
            }
        }
    }
}
