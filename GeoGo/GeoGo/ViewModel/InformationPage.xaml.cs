﻿using System;
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
        private StackLayout propertyStack = new StackLayout { };

        public InformationPage()
        {
            InitializeComponent();
        }

        public InformationPage(GeoData data)
        {
            InitializeComponent();
            geodata = LocalDatabase.GetGeoDataById(data.Id);
            myMap.UiSettings.ZoomControlsEnabled = false;
            myMap.UiSettings.CompassEnabled = false;
            myMap.UiSettings.MyLocationButtonEnabled = true;
            displayBasicGeodataInformation();
            RedirectMapToCurrentLocation();
        }

        protected override void OnAppearing()
        {
            // Loop Over Properties for this Geodata , and print all the properties as Label
            geodata.Properties.ForEach((Property prop) =>
                                   propertyStack.Children.Add(new Label { Text = $"{prop.PropertyName} : {prop.PropertyValue} " })
            );

            DescriptionStack.Children.Add(propertyStack);
            //Update Content
            base.OnAppearing();
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
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserLocation.Latitude, UserLocation.Longitude), Distance.FromMiles(1)));
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

        protected override void OnDisappearing()
        {
            DescriptionStack.Children.Remove(propertyStack);
            propertyStack = new StackLayout { };
            base.OnDisappearing();
        }

        void displayBasicGeodataInformation()
        {
            namelbl.Text = $"Name : {geodata.Name}";
            typelbl.Text = $"Type : {geodata.Type}";
            providerlbl.Text = $"Provider : {geodata.Provider}";
            shapelbl.Text = $"Shape : {geodata.GeometryShape}";

            geodata.Coordinates.ForEach((Coordinate coor) => {
                DropPin(coor.Latitude, coor.Longitude);
            });
        }


        void AddPropBtn_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new InsertPropertyPage(geodata));
        }

        void EmailBtn_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new InsertPropertyPage(geodata));
        }

        async void NavBtn_Clicked(object sender, System.EventArgs e)
        {
            var location = new Location(geodata.Coordinates[0].Latitude, geodata.Coordinates[0].Longitude);
            var options = new MapsLaunchOptions { Name = geodata.Name, MapDirectionsMode = MapDirectionsMode.Walking };

            await Maps.OpenAsync(location, options);
        }

    }
}
