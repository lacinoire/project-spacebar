using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSPACEbar
{
    public class User
    {
        public uint EarnedXP { get; set; }
        public uint CurrentXP { get; set; }
        public string Name { get; set; }
        public List<Skill> Skills { get; set; }

        public string EarnedXPText => EarnedXP + " XP earned";

        public string SkillsListText
        {
            get
            {
                if (Skills.Any())
                {
                    string result = Skills[0].ToString();
                    for (int i = 1; i < Skills.Count; i++)
                    {
                        result += ", " + Skills[i];
                    }
                    return result;
                }
                return "";
            }
        }

        public List<Order> ClaimedOrders { get; }
        public List<Order> FulfilledOrdersWaitingForConfirmation { get; }
        

        public User()
        {
            ClaimedOrders = new List<Order>();
            FulfilledOrdersWaitingForConfirmation = new List<Order>();
            Skills = new List<Skill>();
        }

		public async Task UpdateClaimedOrders()
		{
			var store = App.DataStore;
			ClaimedOrders.Clear();
			ClaimedOrders.AddRange(await store.GetOrders(this, OrderFilter.Claimed));
		}

		public async Task UpdateFulfilledOrdersWaitingForConfirmation()
		{
			var store = App.DataStore;
			FulfilledOrdersWaitingForConfirmation.Clear();
			FulfilledOrdersWaitingForConfirmation.AddRange((await store.GetOrders(this, OrderFilter.Claimed)).Where(o => o.IsFinished && !o.IsApproved));
		}
	}
}