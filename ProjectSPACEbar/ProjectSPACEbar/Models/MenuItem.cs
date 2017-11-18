using System;

namespace ProjectSPACEbar
{
    public class MenuItem
    {
		public uint id { get; set; }
        public string name { get; set; }
        public uint xp { get; set; }
		/// <summary>
		/// In Euro
		/// </summary>
        public decimal cost { get; set; }
		/// <summary>
		/// In liter
		/// </summary>
		public decimal size { get; set; }

        public string Text => size + "l " + name;
        public string DetailText => "Cost: " + cost + "€, Gain: " + xp + " XP";
	}

}