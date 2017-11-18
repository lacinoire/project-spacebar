using System;
using System.Collections.Generic;

namespace ProjectSPACEbar
{
    public class User
    {
        public List<Order> ClaimedOrders { get; }
        public List<Order> FulfilledOrdersWaitingForConfirmation { get; }

        public User()
        {
            ClaimedOrders = new List<Order>();
            FulfilledOrdersWaitingForConfirmation = new List<Order>();
        }
    }
}
