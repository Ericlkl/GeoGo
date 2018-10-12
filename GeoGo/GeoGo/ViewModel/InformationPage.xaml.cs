using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using GeoGo.Model;


namespace GeoGo.ViewModel
{
    public partial class InformationPage : ContentPage
    {
        public GeoData geodata;
        public InformationPage()
        {
            InitializeComponent();
        }
        public InformationPage(GeoData data)
        {
            InitializeComponent();
            geodata = data;
            namelbl.Text = $"Name : {data.Name}";
            typelbl.Text = $"Type : {data.Type}";
            providerlbl.Text = $"Provider : {data.Provider}" ;
            shapelbl.Text = $"Shape : {data.GeometryShape}";

            //// Loop over the Coordinates List , and generate it to label. finally, put it in the stackLayout
            var index = 1;

            data.Coordinates.ForEach( (Coordinate coor) => {
                var label = new Label { Text = $"Coordinate {index} : {coor.Latitude} , {coor.Longitude}" };
                mainStack.Children.Add(label);
                index++;
            });

            mainStack.Children.Add(AddButtonSet());

             //Update Content
            this.Content = mainStack;
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
