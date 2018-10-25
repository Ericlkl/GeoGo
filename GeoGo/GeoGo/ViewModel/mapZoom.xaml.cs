using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using GeoGo.Model;
using GeoGo.ViewModel;
using GeoGo.styles;
using Xamarin.Essentials;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoGo.ViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class mapZoom : ContentPage
	{
        private static GeoData geodata;
        public mapZoom ()
		{
			InitializeComponent ();
		}
        public mapZoom(GeoData data)
        {
            InitializeComponent();
            geodata = LocalDatabase.GetGeoDataById(data.Id);

            // Map Set Up function
            myMap.UiSettings.ZoomControlsEnabled = false;
            myMap.UiSettings.CompassEnabled = false;
            myMap.UiSettings.MyLocationButtonEnabled = true;
            displayBasicGeodataInformation();
            RedirectMapToCurrentLocation();
        }

        void MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
        {
            RedirectMapToCurrentLocation();
        }

        // Function for direct the map back to user location
        void RedirectMapToCurrentLocation()
        {
            // Update Current Location
            UserLocation.UpdateMyCoordinate();
            // Redirect the map to user current location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitude, Longitude), Distance.FromMiles(1)));
        }


        //Function for Drop pin on the map 
        void DropPin(double lat, double lon)
        {
            var position = new Position(lat, lon); // Latitude, Longitude

            var pin = new Pin()
            {
                Label = "",
                Address = String.Format("latitude : {0:F3}, longitude : {1:F3}",
                         lat, lon),
                Type = PinType.Generic,
                Position = new Position(lat, lon)
            };

            myMap.Pins.Add(pin);

        }

        void DrawLine(List<Coordinate> coorList)
        {

            Polyline myLine = new Polyline();

            coorList.ForEach((Coordinate obj) => myLine.Positions.Add(new Position(obj.Latitude, obj.Longitude)));
            myLine.IsClickable = true;
            myLine.StrokeColor = Color.Accent;

            myLine.StrokeWidth = 5f;
            myLine.Tag = "POLYLINE"; // Can set any object

            myMap.Polylines.Add(myLine);

        }
        double Latitude;
        double Longitude;
        void displayBasicGeodataInformation()
        {
          
            geodata.Coordinates.ForEach((Coordinate coor) => {
                Latitude = coor.Latitude;
                Longitude = coor.Longitude;
                DropPin(Latitude, Longitude);
            });

            if (geodata.GeometryShape == "Line")
            {
                DrawLine(geodata.Coordinates);
            }
            else if (geodata.GeometryShape == "Polygon")
            {
                DrawPolygon(geodata.Coordinates);
            }

        }
        void DrawPolygon(List<Coordinate> coorList)
        {

            Polygon myPolygon = new Polygon();
            coorList.ForEach((Coordinate obj) => myPolygon.Positions.Add(new Position(obj.Latitude, obj.Longitude)));

            myPolygon.IsClickable = true;
            myPolygon.StrokeColor = Color.Accent;
            myPolygon.StrokeWidth = 3f;
            myPolygon.FillColor = Color.FromRgba(255, 0, 0, 64);
            myPolygon.Tag = "POLYGON"; // Can set any object
            myMap.Polygons.Add(myPolygon);

        }
        async void NavBtn_Clicked(object sender, System.EventArgs e)
        {
            var location = new Location(geodata.Coordinates[0].Latitude, geodata.Coordinates[0].Longitude);
            var options = new MapsLaunchOptions { Name = geodata.Name, MapDirectionsMode = MapDirectionsMode.Walking };

            await Maps.OpenAsync(location, options);
        }

        async private void OncurrentLocationClicked(object sender, EventArgs e)
        {
            RedirectMapToCurrentLocation();
        }
    }
}