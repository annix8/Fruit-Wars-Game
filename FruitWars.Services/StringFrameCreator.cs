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
        public IFrame CreateFrame(GameState gameState)
        {
            var boardObjectMapper = new BoardObjectToSymbolMapper();
            var stringBuilder = new StringBuilder();
            Board board = gameState.Board;

            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Cols; col++)
                {
                    BoardObject boardObject = board[row, col];
                    string symbol;
                    if (boardObject is Warrior warrior)
                    {
                        symbol = gameState.WarriorPositionsByPlayerNumber
                            .First(x => x.Value.Item1 == row && x.Value.Item2 == col).Key.ToString();
                    }
                    else
                    {
                        symbol = boardObjectMapper.GetSymbol(boardObject, gameState.CurrentPlayerNumber);
                    }

                    stringBuilder.Append(symbol);
                }
                stringBuilder.AppendLine();
            }

            string playersMessages = string.Join("\n", gameState.Players);
            stringBuilder.AppendLine(playersMessages);

            if (!gameState.GameFinished)
            {
                stringBuilder.Append($"Player{gameState.CurrentPlayerNumber}, make a move please!");
            }
            else
            {
                // todo if the game is finished, display winner
            }

            return new StringFrame(stringBuilder.ToString());
        }
    }
}
