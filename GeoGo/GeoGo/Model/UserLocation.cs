using System;
using Xamarin.Essentials;

namespace GeoGo.Model
{
    public class UserLocation
    {
        public double latitude;
        public double longitude;
        public static UserLocation mylocation = new UserLocation();

        public UserLocation()
        {
            this.UpdateMyCoordinate();
        } 

        public async void UpdateMyCoordinate()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    this.latitude = location.Latitude;
                    this.longitude = location.Longitude;
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


    }
}
