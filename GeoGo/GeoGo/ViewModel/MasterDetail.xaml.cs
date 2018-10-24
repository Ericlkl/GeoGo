﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoGo.Model;

namespace GeoGo.ViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetail : MasterDetailPage
	{
		public MasterDetail ()
		{
			InitializeComponent ();
            masterpage.ListView.ItemSelected += OnItemSelected;

		}

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuItem;
            if(item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.targetType))
                {
                    BarBackgroundColor = Color.FromHex("#454F63"),
                    BarTextColor = Color.FromHex("#ffffff"),

                };

                masterpage.ListView.SelectedItem = null;
                IsPresented = false;
            }

        }
	}
}