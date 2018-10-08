using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GeoGo.Model;

namespace GeoGo.ViewModel
{
    public partial class InsertDataPage : ContentPage
    {
        public InsertDataPage()
        {
            InitializeComponent();
        }

        void AutoFillBtn_Clicked(object sender, System.EventArgs e)
        {
            latitude_Entry.Text = $"{UserLocation.mylocation.latitude}";
            longitude_Entry.Text = $"{UserLocation.mylocation.longitude}";
        }


        void SubmitBtn_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
