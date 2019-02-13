using FruitWars.Contracts;
using FruitWars.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FruitWars.Services
{
    public class StringFrameCreator : IFrameCreator
    {
        public IFrame CreateFrame(GameState gameState)
        {
            // todo make mapping of the board objects and their console char representations
            // todo write real symbols for game objects
            // todo if the game is finished, display winner
            var boardObjectMapper = new BoardObjectToSymbolMapper();
            var stringBuilder = new StringBuilder();
            Board board = gameState.Board;
            List<Player> players = gameState.PlayersByPlayerNumber.Select(x => x.Value).ToList();
            
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Cols; j++)
                {
                    BoardObject boardObject = board[i, j];
                    char symbol = boardObjectMapper.GetSymbol(boardObject);
                    stringBuilder.Append(symbol);
                }
                stringBuilder.AppendLine();
            }

            string playersMessages = string.Join("\n", players);
            stringBuilder.AppendLine(playersMessages);
            return new StringFrame(stringBuilder.ToString());
        }
    }
}
