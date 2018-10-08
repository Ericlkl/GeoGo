using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using GeoGo.Model;

namespace GeoGo
{
    public partial class MapPage : ContentPage
    {
    
        public MapPage()
        {
            InitializeComponent();
            RedirectMapToCurrentLocation();
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

        // ReDirect Button clicked
        void Test_Redirect(object sender, System.EventArgs e)
        {
            RedirectMapToCurrentLocation();
        }

        // Test Button Clicked
        void Test_DropPin(object sender, System.EventArgs e)
        {
            // Update Current Location
            UserLocation.mylocation.UpdateMyCoordinate();

            // This function is a tester for droping pin. might change later
            for (double index = 0; index < 0.1; index += 0.01){
                DropPin(UserLocation.mylocation.latitude + index, UserLocation.mylocation.longitude, "Data Name", "Data Description");
            }
        }
    }
}
