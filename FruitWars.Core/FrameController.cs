using FruitWars.Models;
using FruitWars.Models.Contracts;
using System.Collections.Generic;
using System.Text;

namespace FruitWars.Core
{
    public class FrameController
    {
        // creates a frame based on the board's status
        public IFrame CreateFrame(Board board, List<Player> players)
        {
            // todo make mapping of the board objects and their console char representations
            var stringBuilder = new StringBuilder();

            // todo write real symbols for game objects
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Cols; j++)
                {
                    stringBuilder.Append("-");
                }
                stringBuilder.AppendLine();
            }

            string playersMessages = string.Join("\n", players);
            stringBuilder.AppendLine(playersMessages);
            return new StringFrame(stringBuilder.ToString());
        }
    }
}
