using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProjectSPACEbar.Views
{
    public partial class PendingOrderPage : ContentPage
    {
        Order PendingOrder;

        public PendingOrderPage(Order pendingOrder) 
        {
            InitializeComponent();
            PendingOrder = pendingOrder;
            BindingContext = PendingOrder;

        }

        async void OnConfirmClicked(object sender, EventArgs e)
        {
            await App.DataStore.OrderAction(App.CurrentUser, PendingOrder, "approve");
			App.NotifyOrders();
            await Navigation.PopAsync(true);
        }
    }
}
