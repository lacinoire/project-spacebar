using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
    using System.Linq;
    using System.Threading.Tasks;
    using Views;

    public partial class NewOrderPage : ContentPage
    {
        public List<MenuItemViewModel> MenuItems { get; }

        public NewOrderPage()
        {
            InitializeComponent();
            MenuItems = new List<MenuItemViewModel>();
            BindingContext = this;
            Initialize();
        }

        async void Initialize()
        {
            IEnumerable<MenuItem> menuList = (await App.DataStore.GetMenu(App.CurrentUser)).Items;
            MenuItems.AddRange(menuList.Select(m => new MenuItemViewModel(m)
            {
                OnOrderClicked = new Command(async () => await OrderClicked(m)),
            }));
            Menu.ItemsSource = MenuItems;
        }

        async Task OrderClicked(MenuItem menuItem)
        {
            await App.DataStore.CreateOrder(App.CurrentUser, menuItem);
            await Navigation.PushAsync(new PendingOrderPage((await App.DataStore.GetOrders(App.CurrentUser, OrderFilter.Own)).First()));
        }
    }
}
