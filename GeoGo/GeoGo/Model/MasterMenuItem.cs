using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace GeoGo.Model
{
    public class MasterMenuItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Color BackgroundColor { get; set; }
        public Type targetType { get; set; }

        public MasterMenuItem(string Title, string IconSource, Color BackgroundColor, Type targetType)
        {
            this.Title = Title;
            this.IconSource = IconSource;
            this.BackgroundColor = BackgroundColor;
            this.targetType = targetType;
        }
    }
}
