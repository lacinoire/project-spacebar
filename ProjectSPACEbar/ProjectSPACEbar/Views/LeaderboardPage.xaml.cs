using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectSPACEbar.Views
{
    public partial class LeaderboardPage : ContentPage
    {
        private bool isInitializing = false;
        public Leaderboard leaderboard;

        public LeaderboardPage()
        {
            InitializeComponent();
            Initialize();
            App.AllChanged += async () => 
            {
                Initialize();
            };
            App.OrdersChanged += async () =>
            {
                Initialize();
            };
        }

        async Task Initialize()
        {
            if (isInitializing)
            {
                return;
            }
            isInitializing = true;
            this.leaderboard = await App.DataStore.GetLeaderboard();
            LeaderboardList.ItemsSource = this.leaderboard.Users;
            BindingContext = this;
            isInitializing = false;
        }
    }
}