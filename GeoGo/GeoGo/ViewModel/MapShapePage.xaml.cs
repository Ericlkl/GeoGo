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
            UISetUp();
            RedirectMapToLocation("User");
        }

        // Initializer for Information page to use this page display shape on map
        public MapShapePage(GeoData data)
        {
            InitializeComponent();
            UISetUp();
            DrawShapeWithPin(data);

            // Redirect the map the the object location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(data.Coordinates[0].Latitude, data.Coordinates[0].Longitude), Distance.FromMiles(1)));
        }

        // Initializer for InsertDataPage to use this page display shape on map
        public MapShapePage(bool canDrawShape){
            InitializeComponent();
            UISetUp();
            RedirectMapToLocation("User");
            ToolbarItems.Add(new ToolbarItem("CleanShape", "", CleanShape ));
            DrawShapeAble = canDrawShape;
        }


        void UISetUp(){
            myLine = new Polyline();
            myPolygon = null;
            myPolygon = new Polygon();

            myMap.UiSettings.MyLocationButtonEnabled = true;

            myLine.IsClickable = true;
            myLine.StrokeColor = Color.Green;
            myLine.StrokeWidth = 5f;
            myLine.Tag = "POLYLINE"; // Can set any object

            myPolygon.IsClickable = true;
            myPolygon.StrokeColor = Color.Green;
            myPolygon.StrokeWidth = 3f;
            myPolygon.FillColor = Color.FromRgba(255, 0, 0, 64);
        }

        protected override void OnAppearing()
        {
            if (DrawShapeAble == true) {
                DisplayShapeOnMiniMap();
                RedirectMapToLocation("Target");
            }
            base.OnAppearing();
        }

        public void DisplayShapeOnMiniMap()
        {
            if (InsertDataPage.PositionsList.Count == 1)
            {
                DropPin(InsertDataPage.PositionsList[0].Latitude, InsertDataPage.PositionsList[0].Longitude);
            }
            else if (InsertDataPage.PositionsList.Count == 2)
            {
                CleanMap();
                DrawLine(InsertDataPage.PositionsList[1].Latitude, InsertDataPage.PositionsList[1].Longitude);
            }
            else if (InsertDataPage.PositionsList.Count >= 3)
            {
                CleanMap();
                DrawPolygon(InsertDataPage.PositionsList[InsertDataPage.PositionsList.Count - 1].Latitude, InsertDataPage.PositionsList[InsertDataPage.PositionsList.Count - 1].Longitude);
            } else {
                CleanMap();
            }
        }

        // Clean the Map
        void CleanMap(){
            myMap.Polylines.Clear();
            myMap.Polygons.Clear();
            myMap.Pins.Clear();
        }

        // Function for direct the map back to user location
        void RedirectMapToLocation(string toWhere)
        {
            // Update Current Location
            UserLocation.UpdateMyCoordinate();

            if (InsertDataPage.PositionsList.Count == 0 || toWhere == "User")
            {
                // Redirect the map to user current location
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserLocation.Latitude, UserLocation.Longitude), Distance.FromMiles(1)));
            }
            else if (InsertDataPage.PositionsList.Count != 0 && toWhere == "Target")
            {
                // Redirect the map the the object location
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(InsertDataPage.PositionsList[0].Latitude, 
                                                                            InsertDataPage.PositionsList[0].Longitude),
                                                                   Distance.FromMiles(1)));
            }
        }

        // Redirect Button clicked
        void MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
        {
            RedirectMapToLocation("User");
        }

        void CleanShape(){
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
            UISetUp();
        }

        //**** Important Function ****
        void MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            // Function for insertDatapage to draw a Shape 
            if (DrawShapeAble){
                var lat = e.Point.Latitude;
                var lng = e.Point.Longitude;

                drawShape(lat, lng);

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
                CleanMap();
                DrawLine(lat, lon);
            }

            // currently there is two or more coordinate on the list, which means one line or one polygon existed on the map
            else
            {
                CleanMap();
                DrawPolygon(lat, lon);
            }
        }

        // A function for InformationPage to display the data coordinate to a larger screen
        public void DrawShapeWithPin(GeoData data)
        {
            if( data.Coordinates.Count == 1)
            {
                DropPin(data.Coordinates[0].Latitude, data.Coordinates[0].Longitude);
            } 

            else if (data.Coordinates.Count == 2)
            {

                data.Coordinates.ForEach((Coordinate coorIndex) => {
                    DropPin(coorIndex.Latitude, coorIndex.Longitude);
                    myLine.Positions.Add(new Position( coorIndex.Latitude, coorIndex.Longitude ));
                });

                myMap.Polylines.Add(myLine);
            }

            else if (data.Coordinates.Count > 2)
            {

                data.Coordinates.ForEach((Coordinate coorIndex) => {
                    DropPin(coorIndex.Latitude, coorIndex.Longitude);
                    myPolygon.Positions.Add(new Position(coorIndex.Latitude, coorIndex.Longitude));
                });

                myPolygon.Positions.Add( new Position( data.Coordinates[0].Latitude, data.Coordinates[0].Longitude ) );

                myMap.Polygons.Add(myPolygon);

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
            myLine.Positions.Add(InsertDataPage.PositionsList[0]);
            myLine.Positions.Add(new Position(lat, lon));
            myMap.Polylines.Add(myLine);
        }

        void DrawPolygon(double lat, double lon)
        {

            InsertDataPage.PositionsList.ForEach((Position pos) => myPolygon.Positions.Add(pos));
            myPolygon.Positions.Add(new Position(lat, lon));
            myPolygon.Positions.Add(InsertDataPage.PositionsList[0]);
            myMap.Polygons.Add(myPolygon);

        }

    }
}
