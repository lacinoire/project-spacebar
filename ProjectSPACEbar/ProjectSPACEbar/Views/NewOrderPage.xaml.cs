using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
	using System.Collections.ObjectModel;
	using System.Linq;
    using System.Threading.Tasks;
    using Views;

    public partial class NewOrderPage : ContentPage
    {
        public bool IsInitializing;

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        public NewOrderPage()
        {
            InitializeComponent();
            MenuItems = new ObservableCollection<MenuItemViewModel>();
            BindingContext = this;
			//.OrdersChanged += async () => Initialize();
            //Menu.ItemSelected += (object sender, SelectedItemChangedEventArgs args) => Menu.SelectedItem = null;
            Initialize();
        }

        async Task Initialize()
        {
            MenuItems.Clear();
            IEnumerable<MenuItem> menuList = (await App.DataStore.GetMenu(App.CurrentUser)).Items;
			foreach (var m in menuList)
			{
				MenuItems.Add(new MenuItemViewModel(m)
				{
					OnOrderClicked = new Command(async () => await OrderClicked(m)),
				});
            }
            //Menu.ItemsSource = MenuItems;
        }

        async Task OrderClicked(MenuItem menuItem)
        {
            await App.DataStore.CreateOrder(App.CurrentUser, menuItem);
			App.NotifyOrders();
            await Navigation.PushAsync(new PendingOrderPage((await App.DataStore.GetOrders(App.CurrentUser, OrderFilter.Own)).First()));
        }
    }
}
