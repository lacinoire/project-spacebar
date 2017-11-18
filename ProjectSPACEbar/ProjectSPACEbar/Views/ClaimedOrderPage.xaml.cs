using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProjectSPACEbar.Views
{
    public partial class ClaimedOrderPage : ContentPage
    {
        public Order CurrentOrder;

        public ClaimedOrderPage(Order order)
        {
            InitializeComponent();
            CurrentOrder = order;
            BindingContext = CurrentOrder;
        }

        async void OnFulfilledClicked(object sender, EventArgs e)
        {
			try
			{
				await App.DataStore.OrderAction(App.CurrentUser, CurrentOrder, "finish");
			}
			catch
			{
				// TODO Show error toast, otherwise ignore
			}
			App.NotifyOrders();
            await Navigation.PopAsync(true);
        }
    }
}
