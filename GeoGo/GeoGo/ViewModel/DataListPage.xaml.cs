using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeoGo.Model;
using GeoGo.ViewModel;
using SQLite;
using Xamarin.Forms;
using SQLiteNetExtensions.Extensions;

namespace GeoGo
{
    public partial class DataListPage : ContentPage
    {
        public DataListPage()
        {
            InitializeComponent();
        }

        // activate when the user clicked the
        async void AddBtn_Clicked(object sender, System.EventArgs e)
        {
            //DisplayAlert("Success","You clicked the btn","OK");
            var geometryType = await DisplayActionSheet("Please Select geometry type", "Cancel", null, "Point", "Line", "Polygon");

            // Direct to Insert Data Page
            if (geometryType != "Cancel")
                await Navigation.PushAsync(new InsertDataPage(geometryType));
        }

        // When user clicked the GeoData Item on the list
        void ListviewItem_Tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            // Send the selected Item information to description page
            Navigation.PushAsync(new InformationPage( (GeoData)e.Item ));

            // Deactive the selected effect on the list
            ((ListView)sender).SelectedItem = null;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            // Data Binding the listView Itemsource to the Geodata list from Local SQLite Database
            listView.ItemsSource = LocalDatabase.GetAllGeoDataSet();
        }

    }
}
