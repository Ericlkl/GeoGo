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
            UserLocation.mylocation.UpdateMyCoordinate();
            // Redirect the map to user current location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserLocation.mylocation.latitude, UserLocation.mylocation.longitude), Distance.FromMiles(1)));
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
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {

                conn.CreateTable<GeoData>();
                conn.CreateTable<Coordinate>();

                var dataSet = conn.GetAllWithChildren<GeoData>();

                dataSet.ForEach((GeoData obj) =>
                {
                    DropPin(obj.coordinates[0].latitude, obj.coordinates[0].longitude, obj.name, obj.type);
                });
            }
        }

        // ReDirect Button clicked
        void Test_Redirect(object sender, System.EventArgs e)
        {
            RedirectMapToCurrentLocation();
        }

    }
}
