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
        // initializer for this page
        public MapPage()
        {
            InitializeComponent();
            // User interface set up when first initial
            UISetUp();
            // Redirect the map to user current location
            RedirectMapToCurrentLocation();

        }

        // User interface setting up function when this page first run
        void UISetUp(){

            myMap.UiSettings.ZoomControlsEnabled = false;
            myMap.UiSettings.CompassEnabled = false;

            // If it is IOS Device
            if (Device.RuntimePlatform == Device.iOS)
            {
                // Add the margin
                customNav.Margin = new Thickness(16, 36, 16, 0);
            }
            // If it is Andriod Device
            else
            {
                // Add the margin
                customNav.Margin = new Thickness(16, 16, 16, 0);
            }

        }

        // When user come to this page or come back to this page
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Clean the PositionList in InsertDataPage, in order to provide better user experience
            InsertDataPage.PositionsList.Clear();
            InsertDataPage.geometryShape = null;
            // Direct the map back to the user location
            RedirectMapToCurrentLocation();
            // Show all the geodata on map
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
        void DrawPoints(GeoData geoData)
        {
            geoData.Coordinates.ForEach((Coordinate coordinate) =>
                // Drop Pin
                myMap.Pins.Add(new Pin()
                {
                    Type = PinType.Generic,
                    Label = geoData.Name,
                    Address = String.Format("latitude : {0:F3}, longitude : {1:F3}", coordinate.Latitude, coordinate.Longitude),
                    Position = new Position(coordinate.Latitude, coordinate.Longitude)
                })
            );

        }

        // Function for drawing Line on the map
        void DrawLine(GeoData geoData)
        {
            // Polyline object set up
            Polyline myLine = new Polyline {
                IsClickable = true,
                StrokeColor = Color.Blue,
                StrokeWidth = 5f,
                Tag = "POLYLINE"
            };

            // Loop through the coordinate set for this GeoData object and put the coordinate to Polyline 
            geoData.Coordinates.ForEach((Coordinate obj) => myLine.Positions.Add( new Position(obj.Latitude, obj.Longitude) ) );

            // Draw the line on the map
            myMap.Polylines.Add(myLine);

        }

        // function for drawing Polygon on the map
        void DrawPolygon(GeoData geoData)
        {
            // Polygon object set up
            Polygon myPolygon = new Polygon 
            {
                IsClickable = true,
                StrokeColor = Color.Blue,
                StrokeWidth = 3f,
                FillColor = Color.FromRgba(255, 0, 0, 64),
                Tag = "POLYGON"
            };

            // Loop through the coordinate set for this GeoData object and put the coordinate to Polygon 
            geoData.Coordinates.ForEach((Coordinate obj) => myPolygon.Positions.Add( new Position(obj.Latitude, obj.Longitude) ));

            // Draw the polygon on the map
            myMap.Polygons.Add(myPolygon);

        }

        // Function for looping throught all the geoData From LocalDatabase and draw it on the map
        void DisplayAllTheDataFromDatabase()
        {
            // Get All the Geodata from the database and loop it through one by one
            LocalDatabase.GetAllGeoDataSet().ForEach((GeoData data) =>
            {
                // A switch to detect this GeoData belongs to which type of Geometryshape and then draw it on map
                switch(data.GeometryShape)
                {
                    case "Point":
                    case "MultiPoint":
                        DrawPoints(data);
                        break;

                    case "LineString":
                        // Two Coordinate Object Draw Line
                        DrawLine(data);
                        break;

                    case "Polygon":
                        // Three or more Coordinate Object Draw Polygon
                        DrawPolygon(data);
                        break;
                }
            });

        }

        // When the user clicked on the add Button which is on the navigation bar
        private void OnAddClicked(object sender, EventArgs e)
        {
            // Go to the Insert Data Page
            Navigation.PushAsync(new InsertDataPage());
        }

        // When user clicked on the current location button
        private void OncurrentLocationClicked(object sender, EventArgs e)
        {
            // Direct the map back to the user location
            RedirectMapToCurrentLocation();
        }

        // when the user clicked on the hamburger menu which is on the navbar
        private void OnMenuClicked(object sender, EventArgs e)
        {
            // Pops up the menu
            (Application.Current.MainPage as MasterDetail).IsPresented = true;
        }

    }
}
