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

        void SaveBtn_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
