using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GeoGo.Model;
using Xamarin.Forms.GoogleMaps;

namespace GeoGo.ViewModel
{
    public partial class MapShapePage : ContentPage
    {
        private Pin myPin;
        private Polyline myLine;
        private Polygon myPolygon;
        public bool DrawShapeAble = false;

        public MapShapePage()
        {
            InitializeComponent();
        }

        public MapShapePage(bool canDrawShape){
            InitializeComponent();
            RedirectMapToCurrentLocation();
            DrawShapeAble = canDrawShape;
        }

        // Function for direct the map back to user location
        void RedirectMapToCurrentLocation()
        {
            // Update Current Location
            UserLocation.UpdateMyCoordinate();
            // Redirect the map to user current location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserLocation.Latitude, UserLocation.Longitude), Distance.FromMiles(1)));
        }

        // Redirect Button clicked
        void MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
        {
            RedirectMapToCurrentLocation();
        }

        public void CleanPinBtnClicked2()
        {
            // Clean out everything on the map
            myMap.Pins.Clear();
            myMap.Polygons.Clear();
            myMap.Polylines.Clear();

            // Clean out all the Position data
            InsertDataPage.PositionsList.Clear();

            // Clean Map Object
            myLine = null;
            myPin = null;
            myPolygon = null;
        }

        private void CleanPinBtnClicked(object sender, System.EventArgs e)
        {
            // Clean out everything on the map
            myMap.Pins.Clear();
            myMap.Polygons.Clear();
            myMap.Polylines.Clear();

            // Clean out all the Position data
            InsertDataPage.PositionsList.Clear();

            // Clean Map Object
            myLine = null;
            myPin = null;
            myPolygon = null;
        }

        void MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            if (DrawShapeAble){
                var lat = e.Point.Latitude;
                var lng = e.Point.Longitude;
                DisplayAlert("Success", "Pass1", "Okay");

                drawShape(lat, lng);

                DisplayAlert("Success", "PassFinal", "Okay");

                //Save new record to PositionList temporary
                InsertDataPage.PositionsList.Add(new Position(lat, lng));
            }
        }

        public void drawShape(double lat, double lon)
        {
            // If current there is no coordinate on the list
            if (InsertDataPage.PositionsList.Count == 0)
            {
                DropPin(lat, lon);
            }

            // If currently there is just one coordinate on the list, which means one pin on the map
            else if (InsertDataPage.PositionsList.Count == 1)
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
            myPin = new Pin()
            {
                Label = String.Format("latitude : {0:F3}, longitude : {1:F3}",
                         lat, lon),
                Type = PinType.Generic,
                Position = new Position(lat, lon)
            };

            myMap.Pins.Add(myPin);
        }

        void DrawLine(double lat, double lon)
        {

            myLine = new Polyline();

            myLine.Positions.Add(InsertDataPage.PositionsList[0]);
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

            InsertDataPage.PositionsList.ForEach((Position pos) => myPolygon.Positions.Add(pos));

            myPolygon.Positions.Add(new Position(lat, lon));
            myPolygon.Positions.Add(InsertDataPage.PositionsList[0]);

            myPolygon.IsClickable = true;
            myPolygon.StrokeColor = Color.Green;
            myPolygon.StrokeWidth = 3f;
            myPolygon.FillColor = Color.FromRgba(255, 0, 0, 64);
            myPolygon.Tag = "POLYGON"; // Can set any object

            myMap.Polygons.Add(myPolygon);

        }

    }
}
