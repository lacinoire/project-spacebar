using System;

using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public partial class ProfilePage : ContentPage
    {
        public User CurrentUser => App.CurrentUser;

        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = CurrentUser;
			App.OrdersChanged += async () =>
			{
				App.CurrentUser = await App.DataStore.GetUser(App.CurrentUser.Name);
				OnPropertyChanged(nameof(CurrentUser));
			};
        }
    }
}
