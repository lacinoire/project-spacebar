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
            App.CurrentUser.FulfilledOrdersWaitingForConfirmation.Add(CurrentOrder);
            App.CurrentUser.ClaimedOrders.Remove(CurrentOrder);

            await Navigation.PopAsync(true);
        }
    }
}
