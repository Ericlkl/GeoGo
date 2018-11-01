using GeoGo.Model;
using Xamarin.Forms;

namespace GeoGo.ViewModel
{
    public partial class InsertPropertyPage : ContentPage
    {
        // Global Variable for doing something we need to work with this geodata
        private GeoData geodata { set; get; }

        // Initializer for preview screen only
        public InsertPropertyPage()
        {
            InitializeComponent();
        }

        // Initializer for preview screen only
        public InsertPropertyPage(GeoData data)
        {
            InitializeComponent();
            geodata = data; // Save the incoming geodata to save it as global variable
        }

        // When User clicked on the Save Button
        void SaveBtn_Clicked(object sender, System.EventArgs e)
        {
            // If one of the Entry Field is empty
            if (string.IsNullOrWhiteSpace(pname_Entry.Text) || string.IsNullOrWhiteSpace(pvalue_Entry.Text))
            {
                // Display Alert msg to user, notice them they must fill up the entry field
                DisplayAlert("Entry field Empty", "Entry field can not be empty", "Okay");
                return;
            }

            // Save the Property information to thid GeoData, and save the status information to this variable
            var msg = LocalDatabase.InsertPropertyToGeodata( new Property(pname_Entry.Text, pvalue_Entry.Text) ,geodata);

            // Display the databasr commend status, show it to user it is success or fail
            DisplayAlert($"{msg}", $"insert {msg}", "OKay");

            // If the database operation is success
            if (msg == "Success")
                Navigation.PopAsync(); // Go back to pervious page
        }
    }
}
