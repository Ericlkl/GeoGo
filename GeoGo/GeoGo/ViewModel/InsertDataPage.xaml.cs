using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using GeoGo.Model;

namespace GeoGo.ViewModel
{
    public partial class InsertDataPage : ContentPage
    {

        public static List<Position> PositionsList = new List<Position>();


        private Pin myPin;
        private Polyline myLine;
        private Polygon myPolygon;

        public InsertDataPage()
        {
            InitializeComponent();
            myMap.UiSettings.MyLocationButtonEnabled = false;
            RedirectMapToCurrentLocation("User");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RedirectMapToCurrentLocation("Target");
            displayShapeOnMiniMap();
        }

        // Function for direct the map back to user location
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
            RedirectMapToCurrentLocation("User");
        }

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

        void MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            Navigation.PushAsync(new MapShapePage(true));
        }

        public void displayShapeOnMiniMap()
        {

            if (PositionsList.Count == 1)
            {
                DropPin(PositionsList[0].Latitude, PositionsList[0].Longitude);
            }
            else if (PositionsList.Count == 2)
            {
                CleanMap();
                DrawLine(PositionsList[1].Latitude, PositionsList[1].Longitude);
            }
            else if (PositionsList.Count >= 3)
            {
                CleanMap();
                DrawPolygon(PositionsList[PositionsList.Count - 1].Latitude, PositionsList[PositionsList.Count - 1].Longitude);
            } else {
                CleanMap();
            }
        }

        void CleanMap()
        {
            myMap.Polylines.Clear();
            myMap.Polygons.Clear();
            myMap.Pins.Clear();
        }

        public void drawShape(double lat, double lon){
            // If current there is no coordinate on the list
            if ( PositionsList.Count == 0 ){
                DropPin(lat,lon);
            } 

            // If currently there is just one coordinate on the list, which means one pin on the map
            else if ( PositionsList.Count == 1 ){
                myMap.Pins.Clear();
                DrawLine(lat, lon);
            }

            // currently there is two or more coordinate on the list, which means one line or one polygon existed on the map
            else
            {
                myMap.Polylines.Clear();
                myMap.Polygons.Clear();
                DrawPolygon(lat,lon);
            }
        }

        //Function for Drop pin on the map 
        void DropPin(double lat, double lon)
        {
            myPin = new Pin()
            {
                Label = String.Format("latitude : {0:F3}, longitude : {1:F3}",
                         lat, lon),
                Type = PinType.Generic,
                Position = new Position(lat, lon)
            };

            myMap.Pins.Add(myPin);
        }

        void DrawLine(double lat, double lon){

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

            myPolygon.Positions.Add( new Position(lat, lon));
            myPolygon.Positions.Add(PositionsList[0]);

            myPolygon.IsClickable = true;
            myPolygon.StrokeColor = Color.Green;
            myPolygon.StrokeWidth = 3f;
            myPolygon.FillColor = Color.FromRgba(255, 0, 0, 64);
            myPolygon.Tag = "POLYGON"; // Can set any object

            myMap.Polygons.Add(myPolygon);

        }

        void SubmitBtn_Clicked(object sender, System.EventArgs e)
        {

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

            if (PositionsList.Count >= 3)
                coorList.Add(new Coordinate(PositionsList[0].Latitude, PositionsList[0].Longitude));

            GeoData data = new GeoData(name_Entry.Text, type_Entry.SelectedItem.ToString(), User.nickname, description_Entry.Text);

            // Insert the Geodata into SQLite database and recieve the message
            string msg = LocalDatabase.InsertNewGeodataToDB(coorList, data);

            //Display msg to the user
            DisplayAlert($"{msg}", $"GeoData Insert {msg}", "Okay");

            PositionsList.Clear();

            //Go Back to Previous Page
            Navigation.PopAsync();
        }

    }

}
