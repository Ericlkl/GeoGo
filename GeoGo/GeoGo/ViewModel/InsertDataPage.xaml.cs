using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GeoGo.Model;

namespace GeoGo.ViewModel
{
    public partial class InsertDataPage : ContentPage
    {

        public InsertDataPage()
        {
            InitializeComponent();
        }

        void AutoFillBtn_Clicked(object sender, System.EventArgs e)
        {
            UserLocation.UpdateMyCoordinate();
            latitude_Entry.Text = $"{UserLocation.Latitude}";
            longitude_Entry.Text = $"{UserLocation.Longitude}";
        }


        void SubmitBtn_Clicked(object sender, System.EventArgs e)
        {
            // Put all the entry to the list for checking validation
            var allInputEntry = new List<Entry> { name_Entry, type_Entry, provider_Entry, latitude_Entry, longitude_Entry };

            var validationChecker = 0;

            // Check all the entry field is not empty. If Yes display the alert msg to user and end the function
            allInputEntry.ForEach((Entry entry) =>
            {
                if (string.IsNullOrWhiteSpace(entry.Text))
                    validationChecker++;
            });

            if (validationChecker > 0)
            {
                DisplayAlert("Input Field is empty !", $"{validationChecker} Input fields are empty. Please insert the information", "Okay");
                return;
            }
            else
            {
                // using all the information form entry field to create Coordinate and GeoData Object
                Coordinate coor = new Coordinate(Convert.ToDouble(latitude_Entry.Text), Convert.ToDouble(longitude_Entry.Text));

                GeoData data = new GeoData(name_Entry.Text, type_Entry.Text, provider_Entry.Text);

                // Insert the Geodata into SQLite database and recieve the message
                string msg = LocalDatabase.InsertNewGeodataToDB(coor, data);

                //Display msg to the user
                DisplayAlert($"{msg}", $"GeoData Insert {msg}", "Okay");

            }
            //Go Back to Previous Page
            Navigation.PopAsync();
        }

    }

}
