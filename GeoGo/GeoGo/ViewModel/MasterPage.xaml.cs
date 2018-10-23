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
            btnLogout.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));


        }

        async void OnLabelClicked()
        {
            var authenticationService = DependencyService.Get<IAuthenticationService>();
            await authenticationService.LogoutRequest();
            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new LoginPage();
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                await Navigation.PushModalAsync(new LoginPage());
            }
        }

        void SetItems()
        {
            items = new List<MasterMenuItem>();            
            items.Add(new MasterMenuItem("Home", "ic_home.png", Color.White, typeof(MapPage)));
            items.Add(new MasterMenuItem("Offline Info", "ic_cloud.png", Color.White, typeof(DataListPage)));
            ListView.ItemsSource = items;
        }

        async void OnLogoutClicked(object sender, EventArgs args)
        {
            var authenticationService = DependencyService.Get<IAuthenticationService>();
            await authenticationService.LogoutRequest();
            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new LoginPage();
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                await Navigation.PushModalAsync(new LoginPage());
            }
        }
    }
}