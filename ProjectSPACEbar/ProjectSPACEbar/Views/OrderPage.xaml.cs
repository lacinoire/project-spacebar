using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public partial class OrderPage : ContentPage
    {
        List<Order> OpenOrders => App.OpenOrders;

        public OrderPage()
        {
            InitializeComponent();

            BindingContext = this;
             //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewOrderPage, Order>(this, "AddItem", async (obj, item) =>
            //{
            //    var _item = item as Order;
            //    OpenOrders.Add(_item);
            //    await App.DataStore.AddItemAsync(_item);
            //});
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var order = args.SelectedItem as Order;
            if (order == null)
            {
                return;
            }
            await Navigation.PushAsync(new OrderDetailPage(new ItemDetailViewModel(order)));
            ItemsListView.SelectedItem = null;

        }

        async void OnDetailsClicked(object parameter)
        {
            await Navigation.PushAsync(new OrderDetailPage(new ItemDetailViewModel((Order)parameter)));
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
