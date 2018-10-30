using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace GeoGo.Model
{
	public class MasterPageItem
	{
		public string Title { get; set;}
        public string Icon { get; set; }
        public Type targetType { get; set; }

        public MasterPageItem(){}

        public MasterPageItem(string title, string icon, Type type){
            Title = title;
            Icon = icon;
            targetType = type;
        }
	}
}