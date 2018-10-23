using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using GeoGo.Model;

using SQLite;
using SQLiteNetExtensions.Extensions;

namespace GeoGo
{
    public partial class MapPage : ContentPage
    {

        public MapPage()
        {
            InitializeComponent();
            RedirectMapToCurrentLocation();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DisplayAllTheDataFromDatabase();
        }

        // Function for direct the map back to user location
        void RedirectMapToCurrentLocation()
        {
            // Update Current Location
            UserLocation.UpdateMyCoordinate();
            // Redirect the map to user current location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserLocation.Latitude, UserLocation.Longitude), Distance.FromMiles(1)));
        }

        //Function for Drop pin on the map 
        void DropPin(double lat, double lon, string lblName, string description)
        {
            var position = new Position(lat, lon); // Latitude, Longitude

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = lblName,
                Address = description
            };

            myMap.Pins.Add(pin);
        }


        void DisplayAllTheDataFromDatabase()
        {
            // Get All the Geodata from the database and loop it through one by one
            LocalDatabase.GetAllGeoDataSet().ForEach((GeoData obj) =>
            {
                // Droping pin allocating to this geodata
                DropPin(obj.Coordinates[0].Latitude, obj.Coordinates[0].Longitude, obj.Name, obj.Type);
            });

        }

        // ReDirect Button clicked
        void Test_Redirect(object sender, System.EventArgs e)
        {
            RedirectMapToCurrentLocation();
        }
        
        private void OnMenuClicked(object sender, EventArgs e)
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = true;
        }
        
        private void OnAddClicked(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("i am here add");
        }
        private void OnDrawFilterClicked(object sender, EventArgs e)
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = true;
        }

        private void OncurrentLocationClicked(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("i am here add");
        }
    }
}
