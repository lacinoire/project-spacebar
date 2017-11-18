using System;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public class MenuItemViewModel
    {
        public MenuItem MenuItem { get; set; }
        public Command OnOrderClicked { get; set; }

        public MenuItemViewModel(MenuItem mi)
        {
            MenuItem = mi;
        }
    }
}
