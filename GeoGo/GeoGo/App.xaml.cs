using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoGo.ViewModel;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GeoGo
{
    public partial class App : Application
    {
        public static string DatabaseLocation = String.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new LoginPage());
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
