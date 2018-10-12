using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using GeoGo.Model;


namespace GeoGo.ViewModel
{
    public partial class InformationPage : ContentPage
    {
        private static GeoData geodata;
        private StackLayout propertyStack = new StackLayout{};

        public InformationPage()
        {
            InitializeComponent();
        }

        public InformationPage(GeoData data)
        {
            InitializeComponent();
            geodata = LocalDatabase.GetGeoDataById(data.Id);
            displayBasicGeodataInformation();
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

            //// Loop over the Coordinates List , and generate it to label. finally, put it in the stackLayout
            var index = 1;

            geodata.Coordinates.ForEach((Coordinate coor) => {
                DescriptionStack.Children.Add(new Label { Text = $"Coordinate {index} : {coor.Latitude} , {coor.Longitude}" });
                index++;
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
        void NavBtn_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new InsertPropertyPage(geodata));
        }

    }
}
