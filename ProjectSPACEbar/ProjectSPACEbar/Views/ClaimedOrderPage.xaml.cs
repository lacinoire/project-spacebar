using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProjectSPACEbar.Views
{
    public partial class ClaimedOrderPage : ContentPage
    {
        public Order CurrentOrder => App.CurrentUser.ClaimedOrders[0];

        public ClaimedOrderPage(Order order)
        {
            InitializeComponent();
            App.CurrentUser.ClaimedOrders.Insert(0, order);
            BindingContext = CurrentOrder;
        }

        async void OnFulfilledClicked()
        {
            App.CurrentUser.FulfilledOrdersWaitingForConfirmation.Add(CurrentOrder);
            App.CurrentUser.ClaimedOrders.Remove(CurrentOrder);

            await Navigation.PopAsync(true);
        }
    }
}
