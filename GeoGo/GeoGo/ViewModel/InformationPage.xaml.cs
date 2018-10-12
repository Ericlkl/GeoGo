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

        public InformationPage()
        {
            InitializeComponent();
        }

        public InformationPage(GeoData data)
        {
            InitializeComponent();
            geodata = LocalDatabase.GetGeoDataById(data.Id);
        }

        protected override void OnAppearing()
        {
            namelbl.Text = $"Name : {geodata.Name}";
            typelbl.Text = $"Type : {geodata.Type}";
            providerlbl.Text = $"Provider : {geodata.Provider}";
            shapelbl.Text = $"Shape : {geodata.GeometryShape}";

            //// Loop over the Coordinates List , and generate it to label. finally, put it in the stackLayout
            var index = 1;

            geodata.Coordinates.ForEach((Coordinate coor) => {
                mainStack.Children.Add(new Label { Text = $"Coordinate {index} : {coor.Latitude} , {coor.Longitude}" });
                index++;
            });

            // Loop Over Properties for this Geodata , and print all the properties as Label
            geodata.Properties.ForEach((Property prop) =>
                                   mainStack.Children.Add(new Label { Text = $"{prop.PropertyName} : {prop.PropertyValue} " })
            );

            mainStack.Children.Add(AddButtonSet());

            //Update Content
            this.Content = mainStack;
            base.OnAppearing();
        }


        StackLayout AddButtonSet()
        {
            Button addPropBtn = new Button { Text = "Add Properties", WidthRequest=120 };
            addPropBtn.Clicked += (object sender, EventArgs e) => Navigation.PushAsync(new InsertPropertyPage(geodata));

            Button EmailBtn = new Button { Text = "Send Email", WidthRequest = 120 };
            EmailBtn.Clicked += (object sender, EventArgs e) => { };

            Button NavigationBtn = new Button { Text = "Navigation", WidthRequest = 120 };
            NavigationBtn.Clicked += (object sender, EventArgs e) => { };

            StackLayout btnSetStack = new StackLayout { Orientation = StackOrientation.Horizontal };

            btnSetStack.Children.Add(addPropBtn);
            btnSetStack.Children.Add(EmailBtn);
            btnSetStack.Children.Add(NavigationBtn);
            return btnSetStack;
        }
    }
}
