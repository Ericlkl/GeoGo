using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeoGo.Model;
using GeoGo.ViewModel;

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

        // activate when the user clicked the
        async void AddBtn_Clicked(object sender, System.EventArgs e)
        {
            // To-DO : Direct to the InsertDataPage
            //DisplayAlert("Success","You clicked the btn","OK");
            var action = await DisplayActionSheet("Please Select geometry type", "Cancel", null, "Point", "Line", "Polygon");
            Debug.WriteLine("Action: " + action);
            await Navigation.PushAsync(new InsertDataPage());
        }

        // When user clicked the GeoData Item on the list
        void Item_Tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            // TO-DO: Send the selected Item information to description page
            DisplayAlert("Item Clicked", e.Item.ToString(), "Back");
        }

        void ConnectItemSource(){
            // Simple Data example, might be delete later. after the local storage function is works
            List<GeoData> DataList = new List<GeoData> { 
                new GeoData("PineTree1","Planet","Eric Lee", new Coordinate(255.5 , -140.2)),
                new GeoData("PineTree2","Planet","ZiJun Lu", new Coordinate(340.2 , -117.4)),
                new GeoData("PineTree1","Planet","Jeffrey Lau", new Coordinate(300.2 , 340.11))
            };
            // Binding the data to datalist
            listView.ItemsSource = DataList;
        }
    }
}
