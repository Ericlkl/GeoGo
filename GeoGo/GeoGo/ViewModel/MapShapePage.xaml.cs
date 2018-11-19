using System;
using Xamarin.Forms;
using GeoGo.Model;
using Xamarin.Forms.GoogleMaps;

namespace GeoGo.ViewModel
{
    public partial class MapShapePage : ContentPage
    {
        // Global Variable for drawing the pin/line/polygon on the map
        private Pin myPin;
        private Polyline myLine;
        private Polygon myPolygon;

        // Variable for decide the user can they draw shape on the map
        // if the user enter this page from InsertDataPage, they can draw the shape
        // else if the user enter this page from Information Page, they cann't draw the shape
        public bool DrawShapeAble = false;

        public MapShapePage()
        {
            //initialize the Xaml Component
            InitializeComponent();

            // Seting up the User interface element
            UISetUp();

            // Redirect the map to Userlocation at the beginning
            RedirectMapToLocation("User");
        }

        // Initializer for Information page to use this page display shape on map
        public MapShapePage(GeoData data)
        {
            InitializeComponent();
            UISetUp();
            // Draw the shape on the map also drop the pin on every coordinate
            DrawShapeWithPin(data);

            // Redirect the map the the object location
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(data.Coordinates[0].Latitude, data.Coordinates[0].Longitude), Distance.FromMiles(1)));
        }

        // Initializer for InsertDataPage to use this page display shape on map
        public MapShapePage(bool canDrawShape){
            InitializeComponent();
            UISetUp();

            // Add Clean Shape button on the navigation bar, when triger this function
            // it will clean the shape on the map, also clean out the list in InsertDataPage
            ToolbarItems.Add(new ToolbarItem("CleanShape", "", CleanPositionList));
            // allows the user draw shape on the map
            DrawShapeAble = canDrawShape;
        }

        // When the page shows up
        protected override void OnAppearing()
        {
            // If the drawShapeAble is true, it means it is from insertDataPage
            if (DrawShapeAble == true)
            {
                DrawShape();
                RedirectMapToLocation("Target");
            }
            base.OnAppearing();
        }

        void UISetUp(){
            // Redirect the map to user location
            RedirectMapToLocation("User");
            myMap.UiSettings.MyLocationButtonEnabled = true;

        }

        // Clean the shape on the map that user can create a new shape, but no clean the shape data
        void CleanMap(){
            myMap.Polylines.Clear();
            myMap.Polygons.Clear();
            myMap.Pins.Clear();
            ResetMapObjectValue();
        }

        void ResetMapObjectValue(){
            // Clean Map Object
            myLine = null;
            myPin = null;
            myPolygon = null;

            // Set up Line object
            myLine = new Polyline
            {
                IsClickable = true,
                StrokeColor = Color.Green,
                StrokeWidth = 5f,
                Tag = "POLYLINE"
            };

            // Set Up Polygon object
            myPolygon = null;
            myPolygon = new Polygon
            {
                IsClickable = true,
                StrokeColor = Color.Green,
                StrokeWidth = 3f,
                FillColor = Color.FromRgba(255, 0, 0, 64),
                Tag = "Polygon"
            };
        }

        // Clean the shape on the map and clean the position List and global Variable
        // It allows user to draw a new shape on the map
        void CleanPositionList()
        {
            // Clean out everything on the map
            CleanMap();

            // Clean out all the Position data
            InsertDataPage.PositionsList.Clear();
            UISetUp();
        }

        // Function for direct the map back to user location
        void RedirectMapToLocation(string toWhere)
        {
            // Update Current Location
            UserLocation.UpdateMyCoordinate();
            // If there is no shape on the map or we decide to direct the map to the user location
            if (InsertDataPage.PositionsList.Count == 0 || toWhere == "User")
            {
                // Redirect the map to user current location
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserLocation.Latitude, UserLocation.Longitude), Distance.FromMiles(1)));
            }
            // If there is a shape on the map or we want to direct the map to the object
            else if (InsertDataPage.PositionsList.Count != 0 && toWhere == "Target")
            {
                // Redirect the map the the object location
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(InsertDataPage.PositionsList[0].Latitude, 
                                                                            InsertDataPage.PositionsList[0].Longitude),
                                                                   Distance.FromMiles(1)));
            }
        }

        // When the user click on the map
        void MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            // Function for insertDatapage to draw a Shape 
            if (DrawShapeAble){
                // Get the location by user touch on the map
                var lat = e.Point.Latitude;
                var lng = e.Point.Longitude;

                //Save new record to PositionList temporary
                InsertDataPage.PositionsList.Add(new Position(lat, lng));

                // Draw the shape on the map
                DrawShape();

            }

            // Nothing happen if DrawShapeAble didnot turn to true, it can make sure user can not draw shape when they enter from Information Page
        }

        // A Function for InsertData Page drawing the shape on the map
        public void DrawShape()
        {
            CleanMap();

            // If current there is no coordinate on the list
            if (String.Equals(shape_picker.SelectedItem.ToString(),"Point") || InsertDataPage.PositionsList.Count == 1 )
            {
                DrawMultiPoint();
            }

            // If currently there is just one coordinate on the list, which means one pin on the map
            else if ( String.Equals(shape_picker.SelectedItem.ToString(), "LineString") || InsertDataPage.PositionsList.Count == 2)
            {
                DrawLine();
            }

            // currently there is two or more coordinate on the list, which means one line or one polygon existed on the map
            else if (String.Equals(shape_picker.SelectedItem.ToString(), "Polygon"))
            {
                DrawPolygon();
            }
        }

        //Function for Drop multiple pin on the map 
        void DrawMultiPoint()
        {
            // loop through all the position which in the Position list to make the polygon
            InsertDataPage.PositionsList.ForEach((Position pos) => DropPin(pos.Latitude, pos.Longitude) );
        }

        // Function for drawing line
        void DrawLine()
        {
            // Using the old pin and new lat and lon to make a line
            InsertDataPage.PositionsList.ForEach((Position pos) => myLine.Positions.Add(pos));
            // draw a line on the map
            myMap.Polylines.Add(myLine);
        }

        // Function for drawing Polygon
        void DrawPolygon()
        {
            // loop through all the position which in the Position list to make the polygon
            InsertDataPage.PositionsList.ForEach((Position pos) => myPolygon.Positions.Add(pos));

            // add the first position to link the origin point to make it as a Polygon
            myPolygon.Positions.Add(InsertDataPage.PositionsList[0]);
            // put the polygon on the map
            myMap.Polygons.Add(myPolygon);

        }

        //Function for Drop pin on the map 
        void DropPin(double lat, double lon)
        {
            // use latitude and longitute to make a pin variable
            myPin = new Pin()
            {
                Label = String.Format("latitude : {0:F3}, longitude : {1:F3}",
                         lat, lon),
                Type = PinType.Generic,
                Position = new Position(lat, lon)
            };

            // Put it on the map
            myMap.Pins.Add(myPin);
        }

        // A function for InformationPage to display the data coordinate to a larger screen
        // it is only for user which enter this page from Information page
        public void DrawShapeWithPin(GeoData data)
        {
            // if this data only exist one Coordinate which is a Pin Object 
            if( data.Coordinates.Count == 1)
            {
                DropPin(data.Coordinates[0].Latitude, data.Coordinates[0].Longitude);
            }
            // if this data only exist two Coordinate which is a Line Object 
            else if (data.Coordinates.Count == 2)
            {
                // Loop Through the Coordinate list from this object to drop the pin on the map
                data.Coordinates.ForEach((Coordinate coorIndex) => {
                    DropPin(coorIndex.Latitude, coorIndex.Longitude);
                    // Add current coordinate to the line
                    myLine.Positions.Add(new Position( coorIndex.Latitude, coorIndex.Longitude ));
                });

                // draw the polygon on the map
                myMap.Polylines.Add(myLine);
            }
            // if this data exist three or more Coordinate which is a Polygon Object 
            else if (data.Coordinates.Count > 2)
            {
                // Loop Through the Coordinate list from this object to drop the pin on the map
                data.Coordinates.ForEach((Coordinate coorIndex) => {
                    DropPin(coorIndex.Latitude, coorIndex.Longitude);

                    // Add current position to the polygon
                    myPolygon.Positions.Add(new Position(coorIndex.Latitude, coorIndex.Longitude));
                });

                // add the original position to link it back to show it as polygon
                myPolygon.Positions.Add( new Position( data.Coordinates[0].Latitude, data.Coordinates[0].Longitude ) );

                // draw polygon on the map
                myMap.Polygons.Add(myPolygon);

            }
        }

        void Selected_Geometry_Shape(object sender, System.EventArgs e)
        {
            InsertDataPage.geometryShape = shape_picker.SelectedItem.ToString();
            if (InsertDataPage.PositionsList.Count != 0)
                DrawShape();
        }


        // Redirect Button clicked
        void MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
        {
            // Direct map to the user location
            RedirectMapToLocation("User");
        }

    }
}
