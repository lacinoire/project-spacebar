using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public class Order
	{
		public uint Id { get; set; }
		public string CreatorName { get; set; }
        public string AssigneeName { get; set; }
		public bool IsClaimed{ get; set; }
		public bool IsApproved { get; set; }
        public bool IsFinished { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}