using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;

namespace GeoGo
{
    public partial class MapPage : ContentPage
    {

        public MapPage()
        {
            InitializeComponent();
            RedirectMapToCurrentLocation();
        }

        public async void RedirectMapToCurrentLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    myMap.MoveToRegion(MapSpan.FromCenterAndRadius( new Position(location.Latitude, location.Longitude), Distance.FromMiles(0.3) ) );
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

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            RedirectMapToCurrentLocation();
        }
    }
}
