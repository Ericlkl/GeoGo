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
        private List<Position> PositionsList = new List<Position>();
        private Pin myPin;
        private Polyline myLine;
        private Polygon myPolygon;

        private static GeoData geodata;
        private Boolean key;
        private InsertDataPage InsertDataPageInstance;


        public mapZoom (InsertDataPage a)
		{
			InitializeComponent ();
            
            key = true;
            myMap.UiSettings.ZoomControlsEnabled = false;
            myMap.UiSettings.CompassEnabled = false;
            myMap.UiSettings.MyLocationButtonEnabled = true;
            btn_locate.IsVisible = false;
            var resetToolbar = new ToolbarItem
            {
                Name = "Reset",
                Command = new Command(() => CleanPinBtnClicked()),
            };

            InsertDataPageInstance = a;
            this.ToolbarItems.Add(resetToolbar);
            PositionsList = InsertDataPageInstance.PositionsList;
            drawAllShape();
            RedirectMapToCurrentLocation();
            
        }

        private void ConfirmBtn_Clicked()
        {
            InsertDataPageInstance.SetPostionslist(PositionsList);            
        }

        public mapZoom(GeoData data)
        {
            InitializeComponent();
            key = false;
            geodata = LocalDatabase.GetGeoDataById(data.Id);
            btn_locate.IsVisible = true;
            // Map Set Up function
            myMap.UiSettings.ZoomControlsEnabled = false;
            myMap.UiSettings.CompassEnabled = false;
            myMap.UiSettings.MyLocationButtonEnabled = true;
            displayBasicGeodataInformation();
            
            RedirectMapToPinLocation();
        }

        void MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
        {
            RedirectMapToPinLocation();
        }

        void CleanPinBtnClicked()
        {
            // Clean out everything on the map
            myMap.Pins.Clear();
            myMap.Polygons.Clear();
            myMap.Polylines.Clear();

            
            // Clean out all the Position data
            PositionsList.Clear();

            // Clean Map Object
            myLine = null;
            myPin = null;
            myPolygon = null;
            InsertDataPageInstance.CleanPinBtnClicked2();
        }
        // Function for direct the map back to user location
        void RedirectMapToCurrentLocation()
        {
            // Update Current Location
            UserLocation.UpdateMyCoordinate();
            // Redirect the map to user current location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserLocation.Latitude, UserLocation.Longitude), Distance.FromMiles(1)));
        }
        // Function for direct the map back to user location
        void RedirectMapToPinLocation()
        {
            // Update Current Location
            UserLocation.UpdateMyCoordinate();
            // Redirect the map to user current location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitude, Longitude), Distance.FromMiles(1)));
        }

        void drawAllShape()
        {
            foreach (var i in PositionsList){
                drawShape(i.Latitude,i.Longitude);
            }
        }

        void MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            if (key)
            {
                var lat = e.Point.Latitude;
                var lng = e.Point.Longitude;

                drawShape(lat, lng);

                // Save new record to PositionList temporary
                PositionsList.Add(new Position(lat, lng));

                InsertDataPageInstance.drawShape(lat, lng);
                InsertDataPageInstance.PositionsList.Add(new Position(lat, lng));
                //InsertDataPageInstance.SetPostionslist(PositionsList);
            }

        }

        void drawShape(double lat, double lon)
        {
            // If current there is no coordinate on the list
            if (PositionsList.Count == 0)
            {
                DropPin(lat, lon);
            }

            // If currently there is just one coordinate on the list, which means one pin on the map
            else if (PositionsList.Count == 1)
            {
                myMap.Pins.Clear();
                DrawLine(lat, lon);
            }

            // currently there is two or more coordinate on the list, which means one line or one polygon existed on the map
            else
            {
                myMap.Polylines.Clear();
                myMap.Polygons.Clear();
                DrawPolygon(lat, lon);
            }
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

        void DrawLine(double lat, double lon)
        {

            myLine = new Polyline();

            myLine.Positions.Add(PositionsList[0]);
            myLine.Positions.Add(new Position(lat, lon));

            myLine.IsClickable = true;
            myLine.StrokeColor = Color.Green;
            myLine.StrokeWidth = 5f;
            myLine.Tag = "POLYLINE"; // Can set any object

            myMap.Polylines.Add(myLine);

        }

        void DrawPolygon(double lat, double lon)
        {

            myPolygon = null;
            myPolygon = new Polygon();

            PositionsList.ForEach((Position pos) => myPolygon.Positions.Add(pos));

            myPolygon.Positions.Add(new Position(lat, lon));
            myPolygon.Positions.Add(PositionsList[0]);

            myPolygon.IsClickable = true;
            myPolygon.StrokeColor = Color.Green;
            myPolygon.StrokeWidth = 3f;
            myPolygon.FillColor = Color.FromRgba(255, 0, 0, 64);
            myPolygon.Tag = "POLYGON"; // Can set any object

            myMap.Polygons.Add(myPolygon);

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
            RedirectMapToPinLocation();
        }

    }
}