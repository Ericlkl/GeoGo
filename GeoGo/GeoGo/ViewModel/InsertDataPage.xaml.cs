﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using GeoGo.Model;

namespace GeoGo.ViewModel
{
    public partial class InsertDataPage : ContentPage
    {
        public List<Coordinate> coorList = new List<Coordinate>();
        public List<Pin> pinList = new List<Pin>();

        public InsertDataPage()
        {
            InitializeComponent();
            myMap.UiSettings.MyLocationButtonEnabled = true;
            myMap.UiSettings.CompassEnabled = true;

            Providerlbl.Text = $"Provider : {User.nickname}";
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


        void MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
        {
            RedirectMapToCurrentLocation();
        }

        void CleanPinBtnClicked(object sender, System.EventArgs e)
        {
            pinList.ForEach((Pin obj) => myMap.Pins.Remove(obj));

            coorList.Clear();
            pinList.Clear();
        }

        void MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            var lat = e.Point.Latitude;
            var lng = e.Point.Longitude;

            DropPin(lat,lng);
            coorList.Add(new Coordinate(lat, lng) );
        }

        //Function for Drop pin on the map 
        void DropPin(double lat, double lon)
        {
            var position = new Position(lat, lon); // Latitude, Longitude

            var pin = new Pin()
            {
                Label = String.Format("latitude : {0:F3}, longitude : {1:F3}",
                         lat, lon),
                Type = PinType.Generic,
                Position = new Position(lat, lon)
            };

            pinList.Add(pin);

            myMap.Pins.Add(pin);

        }



        void SubmitBtn_Clicked(object sender, System.EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(name_Entry.Text) )
            {
                DisplayAlert("Name Entry Field is empty !", " Please insert the name of the object ", "Okay");
                return;
            }

            if (string.IsNullOrWhiteSpace(type_Entry.Text))
            {
                DisplayAlert("Type Entry Field is empty !", "Please insert the type of the object", "Okay");
                return;
            }

            if (pinList.Count == 0){
                DisplayAlert("Object coordinate undefine!", "Please point out the object coordinate on the map ", "Okay");
                return;
            }

            GeoData data = new GeoData(name_Entry.Text, type_Entry.Text, User.nickname );

            // Insert the Geodata into SQLite database and recieve the message
            string msg = LocalDatabase.InsertNewGeodataToDB(coorList, data);

            //Display msg to the user
            DisplayAlert($"{msg}", $"GeoData Insert {msg}", "Okay");

            //Go Back to Previous Page
            Navigation.PopAsync();
        }

    }

}
