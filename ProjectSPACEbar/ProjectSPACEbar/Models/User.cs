using System;
using System.Collections.Generic;

namespace ProjectSPACEbar
{
    public class User
    {
        public uint EarnedXP { get; set; }
        public uint CurrentXP { get; set; }
        public string Name { get; set; }
        public List<Skill> Skills { get; }

        public string EarnedXPText => EarnedXP + " XP earned";

        public List<Order> ClaimedOrders { get; }
        public List<Order> FulfilledOrdersWaitingForConfirmation { get; }
        

        public User()
        {
            ClaimedOrders = new List<Order>();
            FulfilledOrdersWaitingForConfirmation = new List<Order>();
            Skills = new List<Skill>();
        }
    }
}