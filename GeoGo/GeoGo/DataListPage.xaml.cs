using System;
using System.Collections.Generic;
using GeoGo.Model;

using Xamarin.Forms;

namespace GeoGo
{
    public partial class DataListPage : ContentPage
    {
        public DataListPage()
        {
            InitializeComponent();
            ConnectItemSource();
        }

        void AddBtn_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Success","You clicked the btn","OK");
        }

        void Item_Tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            DisplayAlert(e.Item.ToString(), "Okay", "Back");
        }

        void ConnectItemSource(){
            List<GeoData> DataList = new List<GeoData> { 
                new GeoData("PineTree1","Planet","Eric Lee", new Coordinate(255.5 , -140.2)),
                new GeoData("PineTree2","Planet","ZiJun Lu", new Coordinate(340.2 , -117.4)),
                new GeoData("PineTree1","Planet","Jeffrey Lau", new Coordinate(300.2 , 340.11))
            };
            listView.ItemsSource = DataList;
        }
    }
}
