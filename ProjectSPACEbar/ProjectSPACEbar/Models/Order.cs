using System;

namespace ProjectSPACEbar
{
    public class Order
    {
        public User Creator { get; set; }
        public User Assignee { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFinished { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}