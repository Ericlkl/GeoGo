using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using GeoGo.Model;


namespace GeoGo.ViewModel
{
    public partial class InformationPage : ContentPage
    {
        private static GeoData geodata;

        public InformationPage()
        {
            InitializeComponent();
        }

        public InformationPage(GeoData data)
        {
            InitializeComponent();
            geodata = LocalDatabase.GetGeoDataById(data.Id);
            RedirectMapToTargetLocation();
            UISetUp();
            displayBasicGeodataInformation();
        }

        void UISetUp()
        {
            // Map Set Up function
            myMap.UiSettings.ZoomControlsEnabled = false;
            myMap.UiSettings.CompassEnabled = false;
            myMap.UiSettings.MyLocationButtonEnabled = false;
        }

        // It will runs when user come to this page
        protected override void OnAppearing()
        {
            RedirectMapToTargetLocation();

            //Loop Over Properties for this Geodata , and print all the properties as Label
            geodata.Properties.ForEach((Property prop) => {

                //Create a label to display per propertiy information
                var propertylbl = new Label { Text = $"{prop.PropertyName} : {prop.PropertyValue} ", FontSize = 12, TextColor = Color.FromHex("#68454F63") };

                // Put the property label into PropStack
                PropStack.Children.Add(propertylbl);
            });

            //Update Content
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Clean out the Properites Stack when the user leave this page
            PropStack.Children.Clear();
            base.OnDisappearing();
        }

        // Function to redirect the map to target location
        void RedirectMapToTargetLocation(){
            // Rediect the map to the target location
            var targetLocation = new Position(geodata.Coordinates[0].Latitude, geodata.Coordinates[0].Longitude);
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(targetLocation, Distance.FromMiles(1)));
        }

        //Function for Drop pin on the map 
        void DropPin(string targetName, double lat, double lon)
        {
            // Pin SetUp
            var pin = new Pin()
            {
                Label = $"{targetName}",
                Address = String.Format("latitude : {0:F3}, longitude : {1:F3}",
                         lat, lon),
                Type = PinType.Generic,
                Position = new Position(lat, lon)
            };

            myMap.Pins.Add(pin);

        }

        //Function for Draw Line on the map 
        void DrawLine(List<Coordinate> coorList)
        {
            // PolyLine SetUp
            Polyline myLine = new Polyline() 
            { 
                    IsClickable = true, 
                    StrokeColor = Color.Accent,
                    StrokeWidth = 5f, 
                    Tag = "PolyLine" 
            };

            // Loop Through the Coordinate list, to add new postion to the line
            coorList.ForEach( (Coordinate obj) => myLine.Positions.Add(new Position(obj.Latitude, obj.Longitude) ) );

            // Draw Polyline on the map
            myMap.Polylines.Add(myLine);

        }

        void DrawPolygon(List<Coordinate> coorList)
        {

            // Polygon SetUp
            Polygon myPolygon = new Polygon()
            {
                IsClickable = true,
                StrokeColor = Color.Accent,
                StrokeWidth = 3f,
                FillColor = Color.FromRgba(255, 0, 0, 64),
                Tag = "POLYGON"
            };

            // Loop Through the Coordinate list, to add new postion to the polygon
            coorList.ForEach((Coordinate obj) => myPolygon.Positions.Add(new Position(obj.Latitude, obj.Longitude)));

            // Draw Polygon on the map
            myMap.Polygons.Add(myPolygon);

        }

        void displayBasicGeodataInformation()
        {
            namelbl.Text = $"{geodata.Name}";
            providerlbl.Text = $"Update author: {geodata.Provider}";
            Deslbl.Text = geodata.Description;
            timelbl.Text = $"Last Update : {geodata.LastUpdate}";
            typelbl.Text = $"Type of Data: {geodata.Type}";

            geodata.Coordinates.ForEach((Coordinate coor) => {
                DropPin(geodata.Name, coor.Latitude, coor.Longitude);
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


        void Handle_MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            Navigation.PushAsync( new MapShapePage(geodata) );
        }

        void AddPropBtn_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new InsertPropertyPage(geodata));
        }

        //void EmailBtn_Clicked(object sender, System.EventArgs e)
        //{
        //    Navigation.PushAsync(new InsertPropertyPage(geodata));
        //}

        async void NavBtn_Clicked(object sender, System.EventArgs e)
        {
            var location = new Location(geodata.Coordinates[0].Latitude, geodata.Coordinates[0].Longitude);
            var options = new MapsLaunchOptions { Name = geodata.Name, MapDirectionsMode = MapDirectionsMode.Walking };

            await Maps.OpenAsync(location, options);
        }

    }
}
