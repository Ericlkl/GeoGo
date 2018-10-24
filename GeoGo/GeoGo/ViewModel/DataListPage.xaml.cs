using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeoGo.Model;
using GeoGo.ViewModel;
using SQLite;
using Xamarin.Forms;
using Xamarin.Essentials;
using SQLiteNetExtensions.Extensions;

namespace GeoGo
{
    public partial class DataListPage : ContentPage
    {
        private static GeoData geodata;
        public DataListPage()
        {
            InitializeComponent();
            


        }

        // activate when the user clicked the
        async void AddBtn_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new InsertDataPage());
        }

        // When user clicked the GeoData Item on the list
        void ListviewItem_Tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            // Send the selected Item information to description page
            Navigation.PushAsync(new InformationPage((GeoData)e.Item));

            // Deactive the selected effect on the list
            ((ListView)sender).SelectedItem = null;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Data Binding the listView Itemsource to the Geodata list from Local SQLite Database'
            listView.ItemsSource = LocalDatabase.GetAllGeoDataSet();

        }
        

        void SendListBtn_Clicked(object sender, System.EventArgs e)
        {

        }

        //Just a Beta function for development
        async void CleanBtn_Clicked(object sender, System.EventArgs e)
        {
            string deleteDecision = await DisplayActionSheet("Alert !! Are you sure? All the data will be lose. it can not be recover anymore", "Cancel", null, "Confirm");
            if (deleteDecision == "Confirm")
                LocalDatabase.CleanAllDataInTable();
            OnAppearing();
        }
        async void NavBtn_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}
