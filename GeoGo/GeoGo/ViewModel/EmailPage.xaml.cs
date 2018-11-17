using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using GeoGo.Model;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace GeoGo.ViewModel
{
    public partial class EmailPage : ContentPage
    {
        private GeoData targetData;

        public EmailPage()
        {
            InitializeComponent();
        }

        public EmailPage(GeoData geoData)
        {
            targetData = geoData;
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(this.GetType()).Assembly;
            Stream stream = assembly.GetManifestResourceStream("GeoData.json");

            using (var writer = new System.IO.StreamWriter(stream))
            {
                var json = JsonConvert.SerializeObject(targetData);
                DisplayAlert("Message", json, "Okay");
                writer.WriteLine(json);
            }
        }
    }
}
