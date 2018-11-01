using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoGo.Model;
using GeoGo.ViewModel;


namespace GeoGo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var authenticationService = DependencyService.Get<IAuthenticationService>();

            var loginResult = await authenticationService.Authenticate();
           
            var sb = new StringBuilder();

            // If user login case fail
            if (loginResult.IsError)
            {
                //display the fail msg to the login page , notice user they need to login again
                ResultLabel.Text = "An error occurred during login...";

                sb.AppendLine("An error occurred during login:");
                sb.AppendLine(loginResult.Error);
            }
            else
            {
                // If it is success, display the welcome msg to the login page
                ResultLabel.Text = $"Welcome {loginResult.User.Identity.Name}";
                User.SetLoginResult(loginResult);
                User.DebugResult();

                Application.Current.MainPage = new MasterDetail();
            }
        }
    }
}