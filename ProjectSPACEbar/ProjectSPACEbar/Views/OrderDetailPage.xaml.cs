using System;

using Xamarin.Forms;

namespace ProjectSPACEbar
{
    using Views;

    public partial class OrderDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public OrderDetailPage()
        {
            InitializeComponent();

            var item = new Order
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        public OrderDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void OnClaimClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new ClaimedOrderPage(viewModel.Item));
            Navigation.RemovePage(Navigation.NavigationStack[1]);
        }
    }
}
