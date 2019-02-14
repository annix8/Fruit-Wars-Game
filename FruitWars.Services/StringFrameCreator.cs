using FruitWars.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Warriors;
using System;
using System.Linq;
using System.Text;

namespace FruitWars.Services
{
    public class StringFrameCreator : IFrameCreator
    {
        private readonly BoardObjectToSymbolMapper _boardObjectToSymbolMapper;

        public StringFrameCreator(BoardObjectToSymbolMapper boardObjectToSymbolMapper)
        {
            _boardObjectToSymbolMapper = boardObjectToSymbolMapper;
        }
        
        public IFrame CreateFrame(GameState gameState)
        {
            // TODO: there has to be a mapping somewhere... is there a better way?
            if (gameState is WarriorSelectGameState)
            {
                return CreateWarriorSelectGameStateFrame(gameState as WarriorSelectGameState);
            }
            else if (gameState is InProgressGameState)
            {
                return CreateInProgressGameStateFrame(gameState as InProgressGameState);
            }
            else if (gameState is FinishedGameState)
            {
                return CreateFinishedGameStateFrame(gameState as FinishedGameState);
            }
            else
            {
                throw new ArgumentException($"No such game state {gameState.GetType().Name}");
            }
        }

        private StringFrame CreateWarriorSelectGameStateFrame(WarriorSelectGameState warriorSelectGameState)
        {
            return new StringFrame(warriorSelectGameState.DisplayMessage.Trim());
        }

        private StringFrame CreateInProgressGameStateFrame(InProgressGameState inProgressGameState)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Board board = inProgressGameState.Board;

            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Cols; col++)
                {
                    BoardObject boardObject = board[row, col];
                    string symbol;
                    if (boardObject is Warrior warrior)
                    {
                        symbol = inProgressGameState.WarriorPositionsByPlayerNumber
                            .First(x => x.Value.Item1 == row && x.Value.Item2 == col).Key.ToString();
                    }
                    else
                    {
                        symbol = _boardObjectToSymbolMapper.GetSymbol(boardObject);
                    }

                    stringBuilder.Append(symbol);
                }
                stringBuilder.AppendLine();
            }

            string playersMessages = string.Join("\n", inProgressGameState.Players);
            stringBuilder.AppendLine(playersMessages);
            stringBuilder.Append($"Player{inProgressGameState.CurrentPlayerNumber}, make a move please!");

            return new StringFrame(stringBuilder.ToString());
        }

        private StringFrame CreateFinishedGameStateFrame(FinishedGameState finishedGameState)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (finishedGameState.IsGameDraw)
            {
                stringBuilder.AppendLine("Draw game.");
            }
            else
            {
                Board board = finishedGameState.Board;

                for (int row = 0; row < board.Rows; row++)
                {
                    for (int col = 0; col < board.Cols; col++)
                    {
                        BoardObject boardObject = board[row, col];
                        string symbol;
                        if (boardObject is Warrior warrior)
                        {
                            symbol = finishedGameState.WinnerPlayer.Number.ToString();
                        }
                        else
                        {
                            symbol = _boardObjectToSymbolMapper.GetSymbol(boardObject);
                        }

                        stringBuilder.Append(symbol);
                    }
                    stringBuilder.AppendLine();
                }

                Player winner = finishedGameState.WinnerPlayer;
                string message = $"Player{winner.Number} wins the game.";
                stringBuilder.AppendLine(message);
                stringBuilder.AppendLine(winner.ToString());
            }

            stringBuilder.AppendLine("Do you want to start a rematch? (y/n)");

            return new StringFrame(stringBuilder.ToString());
        }
    }
}
