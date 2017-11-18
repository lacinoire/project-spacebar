using Xamarin.Forms;

namespace ProjectSPACEbar.ViewModels
{
    public class OrderViewModel
    {
		public Order Order { get; set; }
		public Command OnDetailsClicked { get; set; }

		public OrderViewModel(Order o)
		{
			Order = o;
		}
	}
}
