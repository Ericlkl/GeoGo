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
            //DisplayAlert("Success","You clicked the btn","OK");
            var action = await DisplayActionSheet("Please Select geometry type", "Cancel", null, "Point", "Line", "Polygon");

            // Display Selected Action
            await DisplayAlert("You Selected", action, "Ok");

            // Direct to Insert Data Page
            await Navigation.PushAsync(new InsertDataPage());
        }

        // When user clicked the GeoData Item on the list
        void ListviewItem_Tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            // Send the selected Item information to description page
            Navigation.PushAsync(new InformationPage( (GeoData)e.Item ));

            // Deactive the selected effect on the list
            ((ListView)sender).SelectedItem = null;
        }

        void ConnectItemSource(){
            // Simple Data example, might be delete later. after the local storage function is works

            // Point Test
            List<Coordinate> point = new List<Coordinate> {
                new Coordinate(255.5 , -140.2)
            };

            // Line Test
            List<Coordinate> line = new List<Coordinate> {
                new Coordinate(255.5 , -140.2),
                new Coordinate(340.2 , -117.4)
            };

            // Polygon Test
            List<Coordinate> polygon = new List<Coordinate> {
                new Coordinate(255.5 , -140.2),
                new Coordinate(340.2 , -117.4),
                new Coordinate(300.2 , 340.11)
            };

            // Data list init
            List<GeoData> DataList = new List<GeoData> { 
                new GeoData("PineTree1","Planet","Eric Lee", point),
                new GeoData("PineTree2","Planet","ZiJun Lu", line),
                new GeoData("PineTree1","Planet","Jeffrey Lau", polygon)
            };
            // Binding the data to datalist
            listView.ItemsSource = DataList;
        }
    }
}
