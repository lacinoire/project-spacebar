using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectSPACEbar.Views
{
    public partial class LeaderboardPage : ContentPage
    {
        public Leaderboard leaderboard;

        public LeaderboardPage()
        {
            InitializeComponent();
            Initialize();
        }

        async Task Initialize()
        {
            this.leaderboard = await App.DataStore.GetLeaderboard();
            LeaderboardList.ItemsSource = this.leaderboard.Users;
            BindingContext = this;
        }
    }
}