using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GeoGo
{
    public partial class App : Application
    {
        // Database information will be changed accroding to the running platform
        public static string DatabaseLocation = String.Empty;

        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        // Initializer for checking database for the app, it required to enter the database information when Apps run on different platfrom
        public App(string databaseLocation)
        {
            InitializeComponent();
            MainPage = new LoginPage();
            DatabaseLocation = databaseLocation;
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
