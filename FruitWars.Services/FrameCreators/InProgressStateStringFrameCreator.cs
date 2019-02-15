using FruitWars.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.GameState;
using FruitWars.Core.Models.Warriors;
using FruitWars.Services.Contracts;
using System;
using System.Linq;
using System.Text;

namespace FruitWars.Services.FrameCreators
{
    public class InProgressStateStringFrameCreator : IStringGameStateFrameCreator
    {
        private const string PlayerMakesMoveMessage = "Player{0}, make a move please!";
        private readonly BoardObjectToSymbolMapper _boardObjectToSymbolMapper;

        public InProgressStateStringFrameCreator(BoardObjectToSymbolMapper boardObjectToSymbolMapper)
        {
            _boardObjectToSymbolMapper = boardObjectToSymbolMapper;
        }

        public IFrame Create(GameStateBase gameState)
        {
            InProgressGameState inProgressGameState = gameState as InProgressGameState;
            StringBuilder stringBuilder = new StringBuilder();
            DrawBoard(inProgressGameState, stringBuilder);
            DrawPlayerMoveMessage(inProgressGameState, stringBuilder);

            return new StringFrame(stringBuilder.ToString());
        }

        public bool ShouldCreate(GameStateBase gameState)
        {
            return gameState is InProgressGameState;
        }

        private void DrawBoard(InProgressGameState inProgressGameState, StringBuilder stringBuilder)
        {
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
        }

        private void DrawPlayerMoveMessage(InProgressGameState inProgressGameState, StringBuilder stringBuilder)
        {
            string playersMessages = string.Join(Environment.NewLine, inProgressGameState.Players);
            stringBuilder.AppendLine(playersMessages);
            stringBuilder.Append(string.Format(PlayerMakesMoveMessage, inProgressGameState.CurrentPlayerNumber));
        }
    }
}
