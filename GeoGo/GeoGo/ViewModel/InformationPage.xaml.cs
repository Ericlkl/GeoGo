using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GeoGo.Model;

namespace GeoGo.ViewModel
{
    public partial class InformationPage : ContentPage
    {
        public InformationPage(GeoData data)
        {
            InitializeComponent();
            namelbl.Text = String.Format("Name : {0}", data.name);
            typelbl.Text = String.Format("Type : {0}", data.type);
            providerlbl.Text = String.Format("Provider : {0}", data.provider);
            shapelbl.Text = String.Format("Geometry Shape : {0}", data.geometryShape);

            // Loop over the Coordinates List , and generate it to label. finally, put it in the stackLayout
            var index = 1;

            data.coordinates.ForEach( (Coordinate coor) => {
                var label = new Label { Text = $"Coordinate {index} : {coor.latitude} , {coor.longitude}" };
                stack.Children.Add(label);
                index++;
            });

            // *** Looping Description, Description Currently is empty , but this function it works.

            //foreach (var description in data.description)
            //{
            //    var label = new Label { Text = $"{description.Key} : {description.Value}" };
            //    stack.Children.Add(label);
            //}

            // Update Content
            this.Content = stack;
        }

    }
}
