using FruitWars.Core.Models;
using FruitWars.Core.Models.GameState;
using System.Collections.Generic;
using System.Linq;

namespace FruitWars.Core.Controllers
{
    public class GameStateController
    {
        public GameStateBase GameState { get; private set; }

        public void CreateNewGameState()
        {
            GameState = new WarriorSelectGameState();
        }

        public void CreateInProgressGameState()
        {
            GameState = new InProgressGameState();
        }

        public bool IsGameFinished()
        {
            return GameState is FinishedGameState;
        }

        public void AddScreenMessageToWarriorSelectScreen(string message)
        {
            WarriorSelectGameState gameState = GameState as WarriorSelectGameState;
            gameState.AddLineToMessage(message);
        }

        public void AddPlayersToGameState(List<Player> players)
        {
            InProgressGameState gameState = GameState as InProgressGameState;
            gameState.Players = players;
        }

        public void AddBoardToGameState(Board board)
        {
            InProgressGameState gameState = GameState as InProgressGameState;
            gameState.Board = board;
        }

        public void AssignCurrentPlayer(int playerNumber)
        {
            InProgressGameState gameState = GameState as InProgressGameState;
            gameState.CurrentPlayerNumber = playerNumber;
        }

        public void AssignWarriorPositionToPlayer(int playerNumber, int warriorRow, int warriorCol)
        {
            InProgressGameState gameState = GameState as InProgressGameState;
            gameState.WarriorPositionsByPlayerNumber[playerNumber] = (warriorRow, warriorCol);
        }

        public (int, int) GetWarriorPositionsByPlayerNumber(int playerNumber)
        {
            InProgressGameState gameState = GameState as InProgressGameState;
            return gameState.WarriorPositionsByPlayerNumber[playerNumber];

        }

        public int GetPlayerNumberByWarriorPosition(int warriorRow, int warriorCol)
        {
            InProgressGameState gameState = GameState as InProgressGameState;
            return gameState.WarriorPositionsByPlayerNumber
                .First(x => x.Value.Item1 == warriorRow && x.Value.Item2 == warriorCol).Key;
        }

        public void EndGameWithWinner(int winnerPlayerNumber)
        {
            InProgressGameState gameState = GameState as InProgressGameState;
            Player winnerPlayer = gameState.Players.First(x => x.Number == winnerPlayerNumber);
            Board board = gameState.Board;
            GameState = new FinishedGameState(winnerPlayer, board, false);
        }

        public void EndGameWithDraw()
        {
            InProgressGameState gameState = GameState as InProgressGameState;
            Board board = gameState.Board;
            GameState = new FinishedGameState(null, board, true);
        }
    }
}
