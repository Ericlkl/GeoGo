using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using GeoGo.Model;


namespace GeoGo.ViewModel
{
    public partial class InformationPage : ContentPage
    {
        public InformationPage(GeoData data)
        {
            InitializeComponent();
            namelbl.Text = String.Format("Name : {0}", data.Name);
            typelbl.Text = String.Format("Type : {0}", data.Type);
            providerlbl.Text = String.Format("Provider : {0}", data.Provider);
            shapelbl.Text = String.Format("Geometry Shape : {0}", data.GeometryShape);

            // Loop over the Coordinates List , and generate it to label. finally, put it in the stackLayout
            var index = 1;

            data.Coordinates.ForEach( (Coordinate coor) => {
                var label = new Label { Text = $"Coordinate {index} : {coor.Latitude} , {coor.Longitude}" };
                stack.Children.Add(label);
                index++;
            });


            // Update Content
            this.Content = stack;
        }

    }
}
