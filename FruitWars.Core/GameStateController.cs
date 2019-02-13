using FruitWars.Core.Models;

namespace FruitWars.Core
{
    public class GameStateController
    {
        private GameState _gameState;

        public GameState GameState { set => _gameState = value; }

        public void EndGameWithWinner(int winnerPlayerNumber)
        {
            _gameState.GameFinished = true;
            _gameState.WinnerPlayerNumber = winnerPlayerNumber;
        }

        public void EndGameWithDraw()
        {
            _gameState.GameFinished = true;
            _gameState.WinnerPlayerNumber = -1;
        }

        public void AssignCurrentPlayer(int playerNumber)
        {
            _gameState.CurrentPlayerNumber = playerNumber;
        }

        public GameState GetGameState()
        {
            return _gameState;
        }
    }
}
