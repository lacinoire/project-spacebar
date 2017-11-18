using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ProjectSPACEbar.ViewModels;
using System.Linq;

namespace ProjectSPACEbar
{
    public partial class OrderPage : ContentPage
    {
        public List<OrderViewModel> OpenOrders { get; }

        public OrderPage()
        {
            InitializeComponent();

            BindingContext = this;
            OpenOrders = new List<OrderViewModel>();

			App.OrdersChanged += async () => await Initialize();
			Initialize();

            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewOrderPage, Order>(this, "AddItem", async (obj, item) =>
            //{
            //    var _item = item as Order;
            //    OpenOrders.Add(_item);
            //    await App.DataStore.AddItemAsync(_item);
            //});
        }

        async Task Initialize()
        {
			OpenOrders.Clear();
            IEnumerable<Order> orderList = await App.DataStore.GetOrders(App.CurrentUser, OrderFilter.Open);

            OpenOrders.AddRange(orderList.Select(o => new OrderViewModel(o)
            {
                OnDetailsClicked = new Command(async () => await DetailsClicked(o)),
            }));
            OrdersListView.ItemsSource = OpenOrders;
			OnPropertyChanged(nameof(OpenOrders));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var order = args.SelectedItem as Order;
            if (order == null)
                return;
            await Navigation.PushAsync(new OrderDetailPage(order));
            //OrdersListView.SelectedItem = null;
        }

        async Task DetailsClicked(Order order)
        {
            await Navigation.PushAsync(new OrderDetailPage(order));
        }
        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new NewOrderPage());
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (OpenOrders.Count == 0)
            //LoadItemsCommand.Execute(null);
        }

        public Command LoadItemsCommand { get; set; }

        //async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        OpenOrders.Clear();
        //        var items = await App.DataStore.GetItemsAsync(true);
        //        foreach (var item in items)
        //        {
        //            OpenOrders.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}
