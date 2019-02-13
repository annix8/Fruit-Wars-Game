using System.Collections.Generic;

namespace FruitWars.Core.Models
{
    public class GameState
    {
        public GameState()
        {
            GameFinished = false;
            WarriorPositionsByPlayerNumber = new Dictionary<int, (int, int)>();
        }

        public Board Board { get; set; }
        public List<Player> Players { get; set; }
        public Dictionary<int, (int, int)> WarriorPositionsByPlayerNumber { get; private set; }
        public bool GameFinished { get; set; }
        public int WinnerPlayerNumber { get; set; }
        public int CurrentPlayerNumber { get; set; }
    }
}
