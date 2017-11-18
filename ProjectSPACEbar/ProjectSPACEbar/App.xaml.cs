using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public partial class App : Application
    {
        public static IDataStore<Order> DataStore => DependencyService.Get<IDataStore<Order>>() ?? new MockDataStore();

        public static bool UseMockDataStore = true;
        public static string BackendUrl = "http://vps.flakebi.de:8080";
        public static User CurrentUser { get; set; }

        public static List<Order> OpenOrders { get; set; }

        public App()
        {
            InitializeComponent();

            OpenOrders = new List<Order>();
            OpenOrders.Add(new Order
            {
                Id = "1",
                Text = "TestOrder",
                Description = "This is for testing.",
            });
            OpenOrders.Add(new Order
            {
                Id = "2",
                Text = "TestOrder2",
                Description = "This is for testing too.",
            });

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());

        }
    }
}
