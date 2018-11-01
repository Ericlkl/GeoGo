using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoGo.Model;
namespace GeoGo.ViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
        public ListView ListView { get { return listview; } }

		public MasterPage ()
		{
			InitializeComponent ();
            //Initialize the MasterMenuItem
            SetItems();

            // SetUp the user info in the hambuger menu
            picture.Source = ImageSource.FromUri(new Uri(User.picture));
            nickname.Text = User.nickname;
            name.Text = User.name;

            // Make a gesture to save the logout action
            var logoutGesture = new TapGestureRecognizer { Command = new Command(() => LogOut()) };
            // Put this action to the btnLogout label because it is not a button
            btnLogout.GestureRecognizers.Add(logoutGesture);
        }

        async void LogOut()
        {
            // Logout Server by Auth0
            var authenticationService = DependencyService.Resolve<IAuthenticationService>();
            await authenticationService.LogoutRequest();
            // Go back to the login page
            Application.Current.MainPage = new LoginPage();
        }

        void SetItems()
        {
            //Variable to store all the page information which will shows up in hamburger menu
            var Menuitems = new List<MasterMenuItem>
            {
                new MasterMenuItem("Home", "ic_home.png", Color.White, typeof(MapPage)),
                new MasterMenuItem("Offline Info", "ic_cloud.png", Color.White, typeof(DataListPage))
            };        
            // Binding the items Source
            ListView.ItemsSource = Menuitems;
        }

    }
}