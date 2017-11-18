using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProjectSPACEbar.Views
{
    public partial class LeaderboardPage : ContentPage
    {
        Leaderboard leaderboard;

        public LeaderboardPage(Leaderboard leaderboard)
        {
            InitializeComponent();
            this.leaderboard = leaderboard;
            LeaderboardList.ItemsSource = this.leaderboard.Users;
            BindingContext = this;
        }
    }
}
