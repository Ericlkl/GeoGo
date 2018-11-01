using GeoGo.Model;
using GeoGo.ViewModel;
using Xamarin.Forms;

namespace GeoGo
{
    public partial class DataListPage : ContentPage
    {
        public DataListPage()
        {
            InitializeComponent();
        }

        // activate when the user clicked on the cell
        async void AddBtn_Clicked(object sender, System.EventArgs e)
        {
            // Direct the user to this GeoData information page
            await Navigation.PushAsync(new InsertDataPage());
        }


        // When user clicked the GeoData Item on the list
        void ListviewItem_Tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            // Send the selected Item information to description page
            Navigation.PushAsync(new InformationPage((GeoData)e.Item));


            // Deactive the selected animation effect on the list
            ((ListView)sender).SelectedItem = null;
        }

        // When this page appearing it will get all the geoData from database and display on the list 
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Data Binding the listView Itemsource to the Geodata list from Local SQLite Database'
            listView.ItemsSource = LocalDatabase.GetAllGeoDataSet();

        }

        // If use wannt to clean out the geoData in the database
        async void CleanBtn_Clicked(object sender, System.EventArgs e)
        {
            // Display alert message to the user, find out are they really wanna delete all the data
            string deleteDecision = await DisplayActionSheet("Alert !! Are you sure? All the data will be lose. it can not be recover anymore", "Cancel", null, "Confirm");

            // If they selected confirm to delete the data
            if (deleteDecision == "Confirm")
                LocalDatabase.CleanAllDataInTable(); // Clean all the data in Geodata table
            // Refreash the page
            OnAppearing();
        }
    }
}
