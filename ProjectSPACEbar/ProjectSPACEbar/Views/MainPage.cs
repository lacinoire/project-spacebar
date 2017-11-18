using System;

using Xamarin.Forms;

namespace ProjectSPACEbar
{
    using Views;

    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page orderPage, aboutPage, leaderboardPage, newOrderPage, skillsPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    orderPage = new NavigationPage(new OrderPage())
                    {
                        Title = "Orders",
                        Icon = "tab_feed.png",
                    };
                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = "tab_about.png",
                    };
                    leaderboardPage = new NavigationPage(new LeaderboardPage())
                    {
                        Title = "Leaderboard",
                        Icon = "",
                    };
                    newOrderPage = new NavigationPage(new NewOrderPage())
                    {
                        Title = "New Order",
                        Icon = "",
                    };
                    skillsPage = new NavigationPage(new SkillsPage())
                    {
                        Title = "Skills",
                        Icon = "",
                    };

                    break;
                default:
                    orderPage = new OrderPage()
                    {
                        Title = "Orders",
                    };
                    aboutPage = new AboutPage()
                    {
                        Title = "About",
                    };
                    leaderboardPage = new LeaderboardPage()
                    {
                        Title = "Leaderboard",
                    };
                    newOrderPage = new NewOrderPage()
                    {
                        Title = "New Order",
                    };
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
