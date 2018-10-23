using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using GeoGo.Model;

using GeoGo.ViewModel;

using SQLite;
using SQLiteNetExtensions.Extensions;

namespace GeoGo
{
    public partial class MapPage : ContentPage
    {

        public MapPage()
        {
            InitializeComponent();
            myMap.UiSettings.ZoomControlsEnabled = false;
            myMap.UiSettings.CompassEnabled = false;
            RedirectMapToCurrentLocation();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RedirectMapToCurrentLocation();
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

            var pin = new Pin()
            {
                Type = PinType.Generic,
                Label = lblName,
                Address = String.Format("latitude : {0:F3}, longitude : {1:F3}",
                         lat, lon),
                Position = new Position(lat, lon)
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

               
        private void OnMenuClicked(object sender, EventArgs e)
        {
            (Application.Current.MainPage as MasterDetail ).IsPresented = true;
        }
        
        private void OnAddClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InsertDataPage());
        }
        private void OnDrawFilterClicked(object sender, EventArgs e)
        {
            (Application.Current.MainPage as MasterDetail).IsPresented = true;
        }

        private void OncurrentLocationClicked(object sender, EventArgs e)
        {
            RedirectMapToCurrentLocation();
        }
    }
}
