using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth0.OidcClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoGo.Model;
namespace GeoGo.ViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
        public ListView ListView { get { return listview; } }
        public List<MasterMenuItem> items;
		public MasterPage ()
		{
			InitializeComponent ();
            SetItems();
            // User.GetLoginResult.User

            picture.Source = ImageSource.FromUri(new Uri(User.picture));
            nickname.Text = User.nickname;
            name.Text = User.name;



        }

        void SetItems()
        {
            items = new List<MasterMenuItem>();            
            items.Add(new MasterMenuItem("Home", "ic_home.png", Color.White, typeof(DashboardPage)));
            items.Add(new MasterMenuItem("Offline Info", "ic_cloud.png", Color.White, typeof(DashboardPage)));
            ListView.ItemsSource = items;
        }

        async void OnLogoutClicked(object sender, EventArgs args)
        {
            var authenticationService = DependencyService.Get<IAuthenticationService>();
            await authenticationService.LogoutRequest();
            if (Device.OS == TargetPlatform.Android)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if (Device.OS == TargetPlatform.iOS)
            {
                await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
            }
        }
    }
}