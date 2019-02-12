using FruitWars.Contracts.IO;
using FruitWars.Models;
using FruitWars.Models.Contracts;
using System;

namespace FruitWars.IO
{
    public class ConsoleRenderer : IRenderer
    {
        public void RenderBoard(IBoard board)
        {
            // todo make mapping of the board objects and their console char representations
            BoardObject[,] boardField = board.GetBoardField();

            // todo write real symbols for game objects
            for (int i = 0; i < boardField.GetLength(0); i++)
            {
                for (int j = 0; j < boardField.GetLength(1); j++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
            }
        }

        public void RenderMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
