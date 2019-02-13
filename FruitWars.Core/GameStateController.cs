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
        }

        public void EndGameWithDraw()
        {
            _gameState.GameFinished = true;
        }

        public GameState GetGameState()
        {
            return _gameState;
        }
    }
}
