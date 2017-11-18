using System;
using System.Collections.Generic;

namespace ProjectSPACEbar
{
    public class Skill
    {
        public string Name { get; set; }
        public uint XPcost { get; set; }
        public List<Skill> Children { get; }

        public String XPCostText => XPcost + " XP";

        public Skill() => Children = new List<Skill>();
    }

}
