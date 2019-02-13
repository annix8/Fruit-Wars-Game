using FruitWars.Contracts.IO;
using FruitWars.Core.Models.Enums;
using System;

namespace FruitWars.IO
{
    public class ConsolePlayerInputReceiver : IInputReceiver
    {
        public Direction ReceiveDirectionInput()
        {
            ConsoleKeyInfo consoleKey = Console.ReadKey();

            // todo handle different input from arrows
            switch (consoleKey.Key)
            {
                case ConsoleKey.UpArrow:
                    return Direction.Up;
                case ConsoleKey.DownArrow:
                    return Direction.Down;
                case ConsoleKey.LeftArrow:
                    return Direction.Left;
                case ConsoleKey.RightArrow:
                    return Direction.Right;
                default:
                    throw new ArgumentException("Press the arrow keys.");
            }
        }

        public string ReceiveStringInput()
        {
            return Console.ReadLine();
        }
    }
}
