using System.Collections.Generic;

namespace FruitWars.Core.Models
{
    public class GameState
    {
        public Board Board { get; set; }
        public List<Player> Players { get; set; }
        public bool GameFinished { get; set; }
    }
}
