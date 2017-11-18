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
                        Icon = "list.png",
                    };
                    aboutPage = new NavigationPage(new ProfilePage())
                    {
                        Title = "Profile",
                        Icon = "user_male.png",
                    };
                    leaderboardPage = new NavigationPage(new LeaderboardPage(App.Leaderboard))
                    {
                        Title = "Leaderboard",
                        Icon = "bar_chart.png",
                        
                    };
                    newOrderPage = new NavigationPage(new NewOrderPage())
                    {
                        Title = "New Order",
                        Icon = "hand_cursor.png",
                    };
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
                    leaderboardPage = new LeaderboardPage(App.Leaderboard)
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
