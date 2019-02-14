using FruitWars.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Warriors;
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

        // todo magic strings should be at least constants
        public IFrame CreateFrame(GameState gameState)
        {
            StringBuilder stringBuilder = new StringBuilder();
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
                        symbol = _boardObjectToSymbolMapper.GetSymbol(boardObject, gameState.CurrentPlayerNumber);
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
                Player winner = gameState.Players.FirstOrDefault(x => x.Number == gameState.WinnerPlayerNumber);

                // game should be draw
                if (winner == null)
                {
                    stringBuilder.Clear();
                    stringBuilder.Append("Draw game.");
                }
                else
                {
                    string message = $"Player{winner.Number} wins the game.";
                    stringBuilder.AppendLine(message);
                    stringBuilder.AppendLine(winner.ToString());
                }
            }

            return new StringFrame(stringBuilder.ToString());
        }
    }
}
