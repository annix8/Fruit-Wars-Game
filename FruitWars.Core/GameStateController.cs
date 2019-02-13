using FruitWars.Core.Models;

namespace FruitWars.Core
{
    public class GameStateController
    {
        private GameState _gameState;

        public GameStateController(GameState gameState)
        {
            _gameState = gameState;   
        }

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

        public GameState GetGameState()
        {
            return _gameState;
        }
    }
}
