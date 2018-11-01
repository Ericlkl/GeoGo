using System;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using GeoGo.Model;
using GeoGo.ViewModel;
using System.Collections.Generic;

namespace GeoGo
{
    public partial class MapPage : ContentPage
    {

        public MapPage()
        {
            InitializeComponent();
            UISetUp();
            RedirectMapToCurrentLocation();

        }

        void UISetUp(){

            myMap.UiSettings.ZoomControlsEnabled = false;
            myMap.UiSettings.CompassEnabled = false;

            if (Device.RuntimePlatform == Device.iOS)
            {
                customNav.Margin = new Thickness(16, 36, 16, 0);
            }
            else
            {
                customNav.Margin = new Thickness(16, 16, 16, 0);
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InsertDataPage.PositionsList.Clear();
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
        void DropPin(double lat, double lon, string lblName)
        {
            // Drop Pin
            myMap.Pins.Add(
                new Pin()
                {
                    Type = PinType.Generic,
                    Label = lblName,
                    Address = String.Format("latitude : {0:F3}, longitude : {1:F3}", lat, lon),
                    Position = new Position(lat, lon)
                }
            );

        }

        void DrawLine(List<Coordinate> coorList )
        {

            Polyline myLine = new Polyline {
                IsClickable = true,
                StrokeColor = Color.Blue,
                StrokeWidth = 5f,
                Tag = "POLYLINE"
            };

            coorList.ForEach((Coordinate obj) => myLine.Positions.Add(new Position(obj.Latitude, obj.Longitude)));

            myMap.Polylines.Add(myLine);

        }

        void DrawPolygon(List<Coordinate> coorList)
        {
        
            Polygon myPolygon = new Polygon 
            {
                IsClickable = true,
                StrokeColor = Color.Blue,
                StrokeWidth = 3f,
                FillColor = Color.FromRgba(255, 0, 0, 64),
                Tag = "POLYGON"
            };
            coorList.ForEach((Coordinate obj) => myPolygon.Positions.Add( new Position(obj.Latitude, obj.Longitude) ));

            myMap.Polygons.Add(myPolygon);

        }


        void DisplayAllTheDataFromDatabase()
        {
            // Get All the Geodata from the database and loop it through one by one
            LocalDatabase.GetAllGeoDataSet().ForEach((GeoData obj) =>
            {
                // Droping pin allocating to this geodata
                if ( string.Equals(obj.GeometryShape,"Point") ){

                    // Only one Coordinate Drop pin!
                    DropPin(obj.Coordinates[0].Latitude, obj.Coordinates[0].Longitude, obj.Name);
                } 
                else if (string.Equals(obj.GeometryShape, "Line"))
                {
                    // Two Coordinate Object Draw Line
                    DrawLine(obj.Coordinates);

                }
                else if (string.Equals(obj.GeometryShape, "Polygon"))
                {
                    // Three or more Coordinate Object Draw Polygon
                    DrawPolygon(obj.Coordinates);
                }
            });

        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InsertDataPage());
        }

        private void OncurrentLocationClicked(object sender, EventArgs e)
        {
            RedirectMapToCurrentLocation();
        }

        private void OnMenuClicked(object sender, EventArgs e)
        {
            (Application.Current.MainPage as MasterDetail).IsPresented = true;
        }

    }
}
