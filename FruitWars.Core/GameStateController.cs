using FruitWars.Core.Models;
using System.Linq;

namespace FruitWars.Core
{
    public class GameStateController
    {
        public GameState GameState { get; private set; }

        public void CreateNewGameState()
        {
            GameState = new GameState();
        }

        public void EndGameWithWinner(int winnerPlayerNumber)
        {
            GameState.GameFinished = true;
            GameState.WinnerPlayerNumber = winnerPlayerNumber;
        }

        public void EndGameWithDraw()
        {
            GameState.GameFinished = true;
            GameState.WinnerPlayerNumber = -1;
        }

        public void AssignCurrentPlayer(int playerNumber)
        {
            GameState.CurrentPlayerNumber = playerNumber;
        }

        public void AssignWarriorPositionToPlayer(int playerNumber, int warriorRow, int warriorCol)
        {
            GameState.WarriorPositionsByPlayerNumber[playerNumber] = (warriorRow, warriorCol);
        }

        public (int, int) GetWarriorPositionsByPlayerNumber(int playerNumber)
        {
            return GameState.WarriorPositionsByPlayerNumber[playerNumber];
        }

        public int GetPlayerNumberByWarriorPosition(int warriorRow, int warriorCol)
        {
            return GameState.WarriorPositionsByPlayerNumber
                .First(x => x.Value.Item1 == warriorRow && x.Value.Item2 == warriorCol).Key;
        }
    }
}
