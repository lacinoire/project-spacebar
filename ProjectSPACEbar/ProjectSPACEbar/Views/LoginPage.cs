using System;

using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            StackLayout layout = new StackLayout
            {
                Children = {
                    new Label
                    {
                        Text = "Welcome to the SPACEBar!",
                        TextColor = Color.Navy,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 40,
                        HorizontalTextAlignment = TextAlignment.Center,
                    },
                    new Label
                    {
                        Text = "Please choose a Username:",
                        FontSize = 20,
                    }
                },
                Padding = 10,
                Spacing = 5,
            };
            Entry entry = new Entry
            {
                Placeholder = "username",
                PlaceholderColor = Color.AliceBlue,
            };
            Button login = new Button
            {
                Text = "Let's Go!",
                FontSize = 20,
            };
            login.Clicked += async (sender, e) => {
                await App.DataStore.RegisterUser(entry.Text);
                App.CurrentUser = await App.DataStore.GetUser(entry.Text);

                if (Device.RuntimePlatform == Device.iOS)
                    App.Current.MainPage = new MainPage();
                else
                    App.Current.MainPage = new NavigationPage(new MainPage());
            };

            layout.Children.Add(entry);
            layout.Children.Add(login);

            Content = layout;
        }
    }
}

