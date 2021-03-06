﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ProjectSPACEbar.ViewModels;
using System.Linq;
using System.Collections.ObjectModel;

namespace ProjectSPACEbar
{
    public partial class OrderPage : ContentPage
    {
        public ObservableCollection<OrderViewModel> OpenOrders { get; }

        public OrderPage()
        {
            InitializeComponent();

            BindingContext = this;
            OpenOrders = new ObservableCollection<OrderViewModel>();
			OrdersListView.ItemsSource = OpenOrders;

            App.OrdersChanged += async () => await Initialize();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			Initialize();


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

            foreach (var o in orderList)
            {
                OpenOrders.Add(new OrderViewModel(o)
                {
                    OnDetailsClicked = new Command(async () => await DetailsClicked(o)),
                });
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var order = args.SelectedItem as Order;
            if (order == null)
                return;
            await Navigation.PushAsync(new OrderDetailPage(order));
            OrdersListView.SelectedItem = null;
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

            if (OpenOrders.Count == 0)
                LoadItemsCommand.Execute(null);
        }

        public Command LoadItemsCommand { get; set; }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                OpenOrders.Clear();
                IEnumerable<Order> orderList = await App.DataStore.GetOrders(App.CurrentUser, OrderFilter.Open);

                foreach (var o in orderList)
                {
                    OpenOrders.Add(new OrderViewModel(o)
                    {
                        OnDetailsClicked = new Command(async () => await DetailsClicked(o)),
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
