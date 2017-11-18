using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public partial class App : Application
    {
        public static CloudDataStore DataStore => DependencyService.Get<CloudDataStore>();

        public static bool UseMockDataStore = true;
        public static string BackendUrl = "http://vps.flakebi.de:8080";
        public static User CurrentUser { get; set; }

        public static List<Order> OpenOrders { get; set; }
        public static Leaderboard Leaderboard { get; set; }

        public App()
        {
            InitializeComponent();

            OpenOrders = new List<Order>();
            OpenOrders.Add(new Order
            {
                Id = 1,
            });
            OpenOrders.Add(new Order
            {
                Id = 2,
            });
            Leaderboard = new Leaderboard();
            Leaderboard.Users.Add(new User
            {
                Name = "Typ1",
                EarnedXP = 2035,
            });
            Leaderboard.Users.Add(new User
            {
                Name = "Typ6",
                EarnedXP = 739,
            });

            CurrentUser = new User()
            {
                EarnedXP = 5000,
                CurrentXP = 2157,
                Name = "Ich",
            };

			DependencyService.Register<CloudDataStore>();

            MainPage = new NavigationPage(new LoginPage());

        }
    }
}
