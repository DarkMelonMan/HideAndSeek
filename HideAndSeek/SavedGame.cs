using System;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeek
{
    public class SavedGame
    {
        public string PlayerLocation { get; set; }
        public Dictionary<string, string> OpponentLocations { get; set; }
        public List<string> FoundOpponentNames { get; set; }
        public int PlayerMoveNumber { get; set; }
    }
}
