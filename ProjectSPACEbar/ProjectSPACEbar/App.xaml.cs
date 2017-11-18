using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public partial class App : Application
    {
		public delegate void ChangedDelegate();

        public static CloudDataStore DataStore => DependencyService.Get<CloudDataStore>();

		public static event ChangedDelegate AllChanged;
		public static event ChangedDelegate OrdersChanged;

		public static bool UseMockDataStore = true;
        public static string BackendUrl = "http://vps.flakebi.de:8080";
        public static User CurrentUser { get; set; }
		
        public static Leaderboard Leaderboard { get; set; }

		public static void NotifyAll()
		{
			AllChanged?.Invoke();
			OrdersChanged?.Invoke();
		}
		public static void NotifyOrders() => OrdersChanged?.Invoke();

		public App()
        {
            InitializeComponent();

			CurrentUser = new User();

			DependencyService.Register<CloudDataStore>();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
