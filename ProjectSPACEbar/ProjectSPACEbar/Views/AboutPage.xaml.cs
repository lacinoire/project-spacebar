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
        }
    }
}
