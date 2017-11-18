using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public class Order
	{
		public uint Id { get; set; }
		public Lazy<Task<User>> Creator { get; set; }
        public Lazy<Task<User>> Assignee { get; set; }
		public bool IsClaimed{ get; set; }
		public bool IsApproved { get; set; }
        public bool IsFinished { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}