using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth0.OidcClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoGo.Model;
using GeoGo.ViewModel;

namespace GeoGo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IAuth0Client _auth0Client;

        public LoginPage()
        {
            InitializeComponent();
            LoginButton.Clicked += LoginButton_Clicked;

        }



        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var authenticationService = DependencyService.Get<IAuthenticationService>();
            var loginResult = await authenticationService.Authenticate();
           
            var sb = new StringBuilder();
            System.Diagnostics.Debug.WriteLine("i am here now");
            if (loginResult.IsError)
            {
                ResultLabel.Text = "An error occurred during login...";

                sb.AppendLine("An error occurred during login:");
                sb.AppendLine(loginResult.Error);
            }
            else
            {
                ResultLabel.Text = $"Welcome {loginResult.User.Identity.Name}";
                User.SetLoginResult(loginResult);
                User.DebugResult();
                
                if (Device.OS == TargetPlatform.Android)
                {
                    Application.Current.MainPage = new NavigationPage(new MasterDetail());
                }
                else if (Device.OS == TargetPlatform.iOS)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new MasterDetail()));
                }
            }

            System.Diagnostics.Debug.WriteLine(sb.ToString());

        }
    }
}