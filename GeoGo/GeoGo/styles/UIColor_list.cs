using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GeoGo.styles
{
    public static class UIColor_list
    {
            public static List<ColorItem> fullColorList = new List<ColorItem>() {
            };

        // A Function to generate Random UIColor back to the user
        public static Color GetRandomColor()
        {
            Random rnd = new Random();
            var ranNum = rnd.Next(0, (fullColorList.Count - 1));
            rnd = null;

            return fullColorList[ranNum].Color;
        }

        //A function to generate Color and return back to the user
        public static Color GetColorByNumber(int number)
        {
            return fullColorList[number].Color;
        }
    }


    // It is a class for recording the UIColor to the list
    public class ColorItem
    {
        public Color Color { get; set; } = Color.Red;
        public string Name { get; set; }
        public string Representation { get { return this.Color.ToString(); } } 
    }
}
