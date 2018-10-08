using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;

namespace GeoGo
{
    public partial class MapPage : ContentPage
    {
        public double latitude;
        public double longitude;

        public MapPage()
        {
            InitializeComponent();
            UpdateCurrentLocation();
        }

        public async void UpdateCurrentLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    latitude = location.Latitude;
                    longitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine(fnsEx);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Console.WriteLine(pEx);
            }
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine(ex);
            }

        }

        void Test_Redirect(object sender, System.EventArgs e)
        {
            // Update latitude and longitude global value
            UpdateCurrentLocation();
            // Redirect the map to user current location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMiles(1)));
        }


        void Test_DropPin(object sender, System.EventArgs e)
        {
            // This function is a tester for droping pin. might change later
            for (double index = 0; index < 0.1; index += 0.01){
                var position = new Position(latitude + index, longitude); // Latitude, Longitude
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
