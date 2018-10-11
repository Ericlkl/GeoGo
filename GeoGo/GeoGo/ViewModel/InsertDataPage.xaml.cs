using System;
using System.Collections.Generic;
using SQLite;

using Xamarin.Forms;
using GeoGo.Model;
using SQLiteNetExtensions.Extensions;

namespace GeoGo.ViewModel
{
    public partial class InsertDataPage : ContentPage
    {
        public List<Entry> allInputEntry;

        public InsertDataPage(String geometryType)
        {
            InitializeComponent();

            if (geometryType == "Polygon" || geometryType == "Line")
            {
                Label coorLbl = new Label { Text = "Coordinate 2 : " };
                Entry latEntry = new Entry { Placeholder = "latitude : " };
                Entry longEntry = new Entry { Placeholder = "longitude : " };

                List<View> viewSet = new List<View> { coorLbl, latEntry, longEntry };
                viewSet.ForEach((View obj) => CoordinateStack.Children.Add(obj));
            }
        }

        void AutoFillBtn_Clicked(object sender, System.EventArgs e)
        {
            latitude_Entry.Text = $"{UserLocation.mylocation.latitude}";
            longitude_Entry.Text = $"{UserLocation.mylocation.longitude}";
        }


        void SubmitBtn_Clicked(object sender, System.EventArgs e)
        {
            // Put all the entry to the list for checking validation
            allInputEntry = new List<Entry> { name_Entry, type_Entry, provider_Entry, latitude_Entry, longitude_Entry};

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
            
                using (SQLiteConnection db = new SQLiteConnection(App.DatabaseLocation))
                {
                    db.CreateTable<GeoData>();
                    db.CreateTable<Coordinate>();

                    // Copy details to create GeoData
                    Coordinate coor = new Coordinate(Convert.ToDouble(latitude_Entry.Text), Convert.ToDouble(longitude_Entry.Text));
                    db.Insert(coor);

                    GeoData data = new GeoData(
                        name_Entry.Text,
                        type_Entry.Text,
                        provider_Entry.Text
                    );

                    int rows = db.Insert(data);

                    data.InsertCoordinate(new List<Coordinate> { coor }); 

                    db.UpdateWithChildren(data);

                    if (rows > 0)
                        DisplayAlert("Success", "GeoData Insert Successfully", "Okay");
                    else
                        DisplayAlert("Fail", "GeoData Insert Fail", "Okay");

                }
                //Go Back to Previous Page
                Navigation.PopAsync();
            }


        }

    }
}
