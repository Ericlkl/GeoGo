using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoGo.Model;

namespace GeoGo.ViewModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetail : MasterDetailPage
	{
		public MasterDetail ()
		{
			InitializeComponent ();
            masterpage.ListView.ItemSelected += onPageSelected;

		}

        void onPageSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPage = e.SelectedItem as MasterMenuItem;
            // if user selected a page on side menu
            if(selectedPage != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(selectedPage.targetType))
                {
                    BarBackgroundColor = Color.FromHex("#454F63"),
                    BarTextColor = Color.FromHex("#ffffff"),

                };

                // Deselect the selected item
                masterpage.ListView.SelectedItem = null;
                // turn off the side menu
                IsPresented = false;
            }

        }
	}
}