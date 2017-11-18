using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public partial class OrderPage : ContentPage
    {
        ItemsViewModel viewModel;

        public OrderPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new OrderDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new NewOrderPage());
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
