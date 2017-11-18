using System;

using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

		void TestApi(object sender, EventArgs e)
		{
			CloudDataStore store = new CloudDataStore();
			store.RegisterUser("myname");
		}
    }
}
