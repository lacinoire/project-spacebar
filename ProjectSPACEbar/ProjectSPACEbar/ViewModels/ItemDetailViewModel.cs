using System;

namespace ProjectSPACEbar
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Order Item { get; set; }
        public ItemDetailViewModel(Order item = null)
        {
            Title = item?.MenuItem?.name;
            Item = item;
        }
    }
}
