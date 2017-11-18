using System;

using Xamarin.Forms;

namespace ProjectSPACEbar
{
    using Views;

    public partial class OrderDetailPage : ContentPage
    {
        Order order;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public OrderDetailPage()
        {
            InitializeComponent();

            order = new Order
            {
            };

            BindingContext = order;
        }

        public OrderDetailPage(Order order)
        {
            InitializeComponent();
            BindingContext = this.order = order;
        }

        async void OnClaimClicked(object sender, EventArgs e) {
            App.CurrentUser.ClaimedOrders.Insert(0,order);
            App.OpenOrders.Remove(order);
            await Navigation.PushAsync(new ClaimedOrderPage(order));
            Navigation.RemovePage(Navigation.NavigationStack[1]);
        }
    }
}
