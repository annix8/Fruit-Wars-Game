using FruitWars.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.GameState;
using FruitWars.Core.Models.Warriors;
using FruitWars.Services.Contracts;
using System.Text;

namespace FruitWars.Services
{
    public class FinishedGameStateStringFrameCreator : IStringGameStateFrameCreator
    {
        private readonly BoardObjectToSymbolMapper _boardObjectToSymbolMapper;

        public FinishedGameStateStringFrameCreator(BoardObjectToSymbolMapper boardObjectToSymbolMapper)
        {
            _boardObjectToSymbolMapper = boardObjectToSymbolMapper;
        }

        public IFrame Create(GameStateBase gameState)
        {
            FinishedGameState finishedGameState = gameState as FinishedGameState;
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

        public bool ShouldCreate(GameStateBase gameState)
        {
            return gameState is FinishedGameState;
        }
    }
}
