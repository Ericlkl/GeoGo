using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using GeoGo.Model;

namespace GeoGo.ViewModel
{
    public partial class InsertDataPage : ContentPage
    {
        // A static PositionList Value which will be used by MapShape Page to draw the shape in a larger map
        public static List<Position> PositionsList = new List<Position>();
        public static string geometryShape;

        // Global Variable for pin/line/polygon object to draw it on map
        private Pin myPin;
        private Polyline myLine;
        private Polygon myPolygon;

        // Initializer for this page
        public InsertDataPage()
        {
            InitializeComponent();
            // disable the default location button which is embedded in google map
            myMap.UiSettings.MyLocationButtonEnabled = false;
            // Direct the map to user location
            RedirectMapToCurrentLocation("User");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Redirect the map back to the target location
            // If there is no target on the map, will back to the user location
            RedirectMapToCurrentLocation("Target");

            // Display the shape on the mini map on this page
            DisplayShapeOnMiniMap();
        }

        // Redirect the map back to the target location
        // If there is no target on the map, will back to the user location
        void RedirectMapToCurrentLocation(string toWhere)
        {
            // Update Current Location
            UserLocation.UpdateMyCoordinate();

            if (PositionsList.Count == 0 || toWhere == "User"){
                // Redirect the map to user current location
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserLocation.Latitude, UserLocation.Longitude), Distance.FromMiles(1)));
            } else if (PositionsList.Count != 0 && toWhere == "Target") {
                // Redirect the map the the object location
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position( PositionsList[0].Latitude , PositionsList[0].Longitude), Distance.FromMiles(1)));
            }
        }

        // Redirect Button clicked
        void MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
        {
            // Direct the map to user location
            RedirectMapToCurrentLocation("User");
        }

        // When the user clicked on the pin button Clean the shape on the map
        private void CleanPinBtnClicked(object sender, System.EventArgs e)
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
        }

        // when the user clicked on the map, it will direct them to a larger map page to draw the shape
        void MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            // Go to the MapShapePage which allows user to click in the map to draw the geometry shape
            Navigation.PushAsync(new MapShapePage(true));
        }

        // A function to display the shape on this mini map
        public void DisplayShapeOnMiniMap()
        {
            if (PositionsList.Count == 1)
            {
                // If only one coordinate it will be a pin
                DropPin(PositionsList[0].Latitude, PositionsList[0].Longitude);
            }
            else if (PositionsList.Count == 2)
            {
                // Clean the pin we just insert before, because this object will be a polygon or line
                CleanMap();
                // If only two coordinate it will be a line
                DrawLine(PositionsList[1].Latitude, PositionsList[1].Longitude);
            }
            else if (PositionsList.Count >= 3)
            {
                // Clean the pin/line/polygon we just insert before, because this object must be a polygon
                CleanMap();
                DrawPolygon(PositionsList[PositionsList.Count - 1].Latitude, PositionsList[PositionsList.Count - 1].Longitude);
            } else {
                // Clean the pin/line/polygon
                CleanMap();
            }
        }

        // clean all the shape on the map
        void CleanMap()
        {
            myMap.Polylines.Clear();
            myMap.Polygons.Clear();
            myMap.Pins.Clear();
        }

        //Function for Drop pin on the map 
        void DropPin(double lat, double lon)
        {
            // init the pin object
            myPin = new Pin()
            {
                Label = String.Format("latitude : {0:F3}, longitude : {1:F3}",
                         lat, lon),
                Type = PinType.Generic,
                Position = new Position(lat, lon)
            };

            // Drop this pin
            myMap.Pins.Add(myPin);
        }

        void DrawLine(double lat, double lon){

            //init the line object
            myLine = new Polyline
            {
                IsClickable = true,
                StrokeColor = Color.Green,
                StrokeWidth = 5f,
                Tag = "POLYLINE"
            };

            // add the old pin coordinate to line
            myLine.Positions.Add(PositionsList[0]);
            // add the new coordinate to line
            myLine.Positions.Add(new Position(lat, lon));

            //Draw line on the map
            myMap.Polylines.Add(myLine);

        }

        void DrawPolygon(double lat, double lon)
        {
            // Clean the old data
            myPolygon = null;

            //Initialize it to a new Polygon object
            myPolygon = new Polygon()
            {
                IsClickable = true,
                StrokeColor = Color.Green,
                FillColor = Color.FromRgba(255, 0, 0, 64),
                StrokeWidth = 3f,
                Tag = "POLYGON"
            };

            // loop through the positionList to insert all the coordinate to the polygon
            PositionsList.ForEach((Position pos) => myPolygon.Positions.Add(pos));
            // insert the new coordinate to the polygon
            myPolygon.Positions.Add( new Position(lat, lon));

            // insert the first coordinate to link the last coordinate back to the start point
            myPolygon.Positions.Add(PositionsList[0]);

            // Draw polygon on the map
            myMap.Polygons.Add(myPolygon);

        }

        void SubmitBtn_Clicked(object sender, System.EventArgs e)
        {
            // Validation Checker
            if (string.IsNullOrWhiteSpace(name_Entry.Text) )
            {
                DisplayAlert("Name Entry Field is empty !", " Please insert the name of the object ", "Okay");
                return;
            }

            if (string.IsNullOrWhiteSpace(description_Entry.Text))
            {
                DisplayAlert("Description Entry Field is empty !", "Please insert the Description for the object", "Okay");
                return;
            }

            if (PositionsList.Count == 0){
                DisplayAlert("Object coordinate undefine!", "Please point out the object coordinate on the map ", "Okay");
                return;
            }

            List<Coordinate> coorList = new List<Coordinate>();

            PositionsList.ForEach((Position pos) => coorList.Add(new Coordinate( pos.Latitude, pos.Longitude)) );

            // If it is a polygon object we need to insert the first coordinate to the last Coodinate for this GeoData in order to make a shape
            if (PositionsList.Count >= 3 && geometryShape == "Polygon")
                coorList.Add(new Coordinate(PositionsList[0].Latitude, PositionsList[0].Longitude));

            // Create a new GeoData object to store it to database after
            GeoData data = new GeoData(name_Entry.Text, type_Entry.SelectedItem.ToString(), User.nickname, description_Entry.Text , geometryShape);

            // Insert the Geodata into SQLite database and recieve the message
            string msg = LocalDatabase.InsertNewGeodataToDB(coorList, data);

            //Display msg to the user
            DisplayAlert($"{msg}", $"GeoData Insert {msg}", "Okay");

            //Go Back to Previous Page
            Navigation.PopAsync();
        }

    }

}
