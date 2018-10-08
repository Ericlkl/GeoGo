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
        }


        void Test_Redirect(object sender, System.EventArgs e)
        {
            // Update Current Location
            UserLocation.mylocation.UpdateMyCoordinate();
            // Redirect the map to user current location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserLocation.mylocation.latitude, UserLocation.mylocation.longitude), Distance.FromMiles(1)));
        }


        void Test_DropPin(object sender, System.EventArgs e)
        {
            // Update Current Location
            UserLocation.mylocation.UpdateMyCoordinate();

            // This function is a tester for droping pin. might change later
            for (double index = 0; index < 0.1; index += 0.01){
                var position = new Position(UserLocation.mylocation.latitude + index, UserLocation.mylocation.longitude ); // Latitude, Longitude

                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = "Data Name",
                    Address = "Data Description can put here"
                };

                myMap.Pins.Add(pin);
            }
        }
    }
}
