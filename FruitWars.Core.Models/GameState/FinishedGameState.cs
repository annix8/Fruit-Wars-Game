namespace FruitWars.Core.Models.GameState
{
    public class FinishedGameState : GameStateBase
    {
        public FinishedGameState(Player winnerPlayer, Board board,  bool isGameDraw)
        {
            WinnerPlayer = winnerPlayer;
            Board = board;
            IsGameDraw = isGameDraw;
        }

        /// <summary>
        /// If null, then game was draw
        /// </summary>
        public Player WinnerPlayer { get; private set; }
        public Board Board { get; set; }
        public bool IsGameDraw { get; private set; }
    }
}
