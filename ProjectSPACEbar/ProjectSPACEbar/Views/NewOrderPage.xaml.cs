using System;

using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public partial class NewOrderPage : ContentPage
    {
        public Order Item { get; set; }

        public NewOrderPage()
        {
            InitializeComponent();

            Item = new Order
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}
