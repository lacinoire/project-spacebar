using System;
using System.Collections.Generic;

namespace ProjectSPACEbar
{
    public class SkillGraph
	{
		public List<Skill> All { get; }
		public List<Skill> Basics { get; }

		public SkillGraph()
		{
			All = new List<Skill>();
			Basics = new List<Skill>();
		}
    }

}