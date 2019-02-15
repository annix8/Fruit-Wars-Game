using FruitWars.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.GameState;
using FruitWars.Core.Models.Warriors;
using FruitWars.Services.Contracts;
using System.Text;

namespace FruitWars.Services.FrameCreators
{
    public class FinishedGameStateStringFrameCreator : IStringGameStateFrameCreator
    {
        private const string DrawGameMessage = "Draw game.";
        private const string PlayerWinsMessage = "Player{0} wins the game.";
        private const string RematchMessage = "Do you want to start a rematch? (y/n)";
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
                stringBuilder.AppendLine(DrawGameMessage);
            }
            else
            {
                DrawBoard(finishedGameState, stringBuilder);
                DrawWinnerMessage(finishedGameState, stringBuilder);
            }

            stringBuilder.AppendLine(RematchMessage);

            return new StringFrame(stringBuilder.ToString().Trim());
        }

        public bool ShouldCreate(GameStateBase gameState)
        {
            return gameState is FinishedGameState;
        }

        private void DrawBoard(FinishedGameState finishedGameState, StringBuilder stringBuilder)
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
        }

        private void DrawWinnerMessage(FinishedGameState finishedGameState, StringBuilder stringBuilder)
        {
            Player winner = finishedGameState.WinnerPlayer;
            string message = string.Format(PlayerWinsMessage, winner.Number);
            stringBuilder.AppendLine(message);
            stringBuilder.AppendLine(winner.ToString());
        }
    }
}
