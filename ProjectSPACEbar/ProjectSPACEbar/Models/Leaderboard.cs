using System.Collections.Generic;

namespace ProjectSPACEbar
{
    public class Leaderboard
    {
        public List<User> Users { get; }

        public Leaderboard() => Users = new List<User>();
    }
}