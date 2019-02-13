using System.Collections.Generic;

namespace FruitWars.Core.Models
{
    public class GameState
    {
        public GameState(Board board, Dictionary<int, Player> playersByPlayerNumber)
        {
            Board = board;
            PlayersByPlayerNumber = playersByPlayerNumber;
            GameFinished = false;
        }

        public Board Board { get; set; }
        public Dictionary<int, Player> PlayersByPlayerNumber { get; set; }
        public bool GameFinished { get; set; }
        public int WinnerPlayerNumber { get; set; }
    }
}
