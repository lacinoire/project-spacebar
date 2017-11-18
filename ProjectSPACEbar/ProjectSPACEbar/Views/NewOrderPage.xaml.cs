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
			App.OrdersChanged += async () => await Initialize();
            Initialize();
        }

        async Task Initialize()
        {
			MenuItems.Clear();
            IEnumerable<MenuItem> menuList = (await App.DataStore.GetMenu(App.CurrentUser)).Items;
            MenuItems.AddRange(menuList.Select(m => new MenuItemViewModel(m)
            {
                OnOrderClicked = new Command(async () => await OrderClicked(m)),
            }));
            Menu.ItemsSource = MenuItems;
			OnPropertyChanged(nameof(MenuItems));
        }

        async Task OrderClicked(MenuItem menuItem)
        {
            await App.DataStore.CreateOrder(App.CurrentUser, menuItem);
			App.NotifyOrders();
            await Navigation.PushAsync(new PendingOrderPage((await App.DataStore.GetOrders(App.CurrentUser, OrderFilter.Own)).First()));
        }
    }
}
