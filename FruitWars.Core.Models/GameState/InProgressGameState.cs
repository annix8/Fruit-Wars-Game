using System.Collections.Generic;

namespace FruitWars.Core.Models.GameState
{
    public class InProgressGameState : GameStateBase
    {
        public InProgressGameState()
        {
            WarriorPositionsByPlayerNumber = new Dictionary<int, (int, int)>();
        }

        public Board Board { get; set; }
        public List<Player> Players { get; set; }
        public Dictionary<int, (int, int)> WarriorPositionsByPlayerNumber { get; private set; }
        public int CurrentPlayerNumber { get; set; }
    }
}
