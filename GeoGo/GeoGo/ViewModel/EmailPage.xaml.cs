using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;

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

        void SendBtn_Clicked(object sender, System.EventArgs e)
        {
            //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GeoData.json");

            ////var assembly = IntrospectionExtensions.GetTypeInfo(this.GetType()).Assembly;
            ////Stream stream = assembly.GetManifestResourceStream("GeoData.json");

            //using (var writer = new System.IO.StreamWriter(stream))
            //{
            //    var json = JsonConvert.SerializeObject(targetData);
            //    DisplayAlert("Message", json, "Okay");
            //    writer.WriteLine(json);
            //}

            var json = JsonConvert.SerializeObject(targetData);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "GeoData.txt");

            // Write , the second parameter determine overwrite the file or not
            using (var streamWriter = new StreamWriter(filename, false))
            {
                streamWriter.Write(json);
            }
            // Read 
            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(content);
                DisplayAlert("Message", content, "Okay");
            }

        }

    }
}
