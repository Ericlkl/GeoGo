using System;
using System.Collections.Generic;
using GeoGo.Model;
using Xamarin.Forms;

namespace GeoGo.ViewModel
{
    public partial class InsertPropertyPage : ContentPage
    {
        private GeoData geodata { set; get; }

        public InsertPropertyPage()
        {
            InitializeComponent();
        }

        public InsertPropertyPage(GeoData data)
        {
            InitializeComponent();
            geodata = data;
        }

        async void SaveBtn_Clicked(object sender, System.EventArgs e)
        {
            // If one of the Entry Field is empty
            if (string.IsNullOrWhiteSpace(pname_Entry.Text) || string.IsNullOrWhiteSpace(pvalue_Entry.Text))
            {
                DisplayAlert("Entry field Empty", "Entry field can not be empty", "Okay");
                return;
            }
            
            var msg = LocalDatabase.InsertPropertyToGeodata( new Property(pname_Entry.Text, pvalue_Entry.Text) ,geodata);
            DisplayAlert($"{msg}", $"insert {msg}", "OKay");

            if (msg == "Success")
                Navigation.PopAsync();
        }
    }
}
