using System;
using System.Linq;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
    using System.Threading.Tasks;
    using Views;

    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Initialize();
        }

        async Task Initialize()
        {
            Page orderPage, aboutPage, leaderboardPage, newOrderPage, skillsPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    orderPage = new NavigationPage(new OrderPage())
                    {
                        Title = "Orders",
                        Icon = "list.png",
                    };
                    aboutPage = new NavigationPage(new ProfilePage())
                    {
                        Title = "Profile",
                        Icon = "user_male.png",
                    };
                    leaderboardPage = new NavigationPage(new LeaderboardPage())
                    {
                        Title = "Leaderboard",
                        Icon = "bar_chart.png",

                    };
                    var orders = await App.DataStore.GetOrders(App.CurrentUser, OrderFilter.Own);
                    if (orders.Any())
                    {
                        Page pending = new PendingOrderPage(orders.First())
                        {
                            Title = "My Order",
                            Icon = "hand_cursor.png",
                        };
                        newOrderPage = new NavigationPage(pending)
                        {
                            Title = "My Order",
                            Icon = "hand_cursor.png",
                        };
                        newOrderPage.Navigation.InsertPageBefore(new NewOrderPage(),pending);
                    }
                    else
                    {
                        newOrderPage = new NavigationPage(new NewOrderPage())
                        {
                            Title = "My Order",
                            Icon = "hand_cursor.png",
                        };
                    }
                    skillsPage = new NavigationPage(new SkillsPage())
                    {
                        Title = "Skills",
                        Icon = "genius.png",
                    };

                    break;
                default:
                    orderPage = new OrderPage()
                    {
                        Title = "Orders",
                    };
                    aboutPage = new ProfilePage()
                    {
                        Title = "Profile",
                    };
                    leaderboardPage = new LeaderboardPage()
                    {
                        Title = "Leaderboard",
                    };
                    var orders2 = await App.DataStore.GetOrders(App.CurrentUser, OrderFilter.Own);
                    if (orders2.Any())
                    {
                        Page pending = new PendingOrderPage(orders2.First())
                        {
                            Title = "My Order",
                        };
                        newOrderPage = new NavigationPage(pending)
						{
							Title = "My Order",
						};
                        newOrderPage.Navigation.InsertPageBefore(new NewOrderPage(), pending);
                    }
                    else
                    {
                        newOrderPage = new NewOrderPage()
                        {
                            Title = "My Order",
                        };
                    }
                    skillsPage = new SkillsPage()
                    {
                        Title = "Skills",
                    };
                    break;
            }

            Children.Add(orderPage);
            Children.Add(newOrderPage);
            Children.Add(skillsPage);
            Children.Add(leaderboardPage);
            Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
