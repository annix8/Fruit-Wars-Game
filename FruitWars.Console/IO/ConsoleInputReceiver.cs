using FruitWars.Contracts.IO;
using FruitWars.Core.Models.Enums;
using System;

namespace FruitWars.IO
{
    public class ConsoleInputReceiver : IInputReceiver
    {
        public Direction ReceiveDirectionInput()
        {
            ConsoleKeyInfo consoleKey = Console.ReadKey();
            
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
                    return Direction.None;
            }
        }

        public string ReceiveStringInput()
        {
            return Console.ReadLine();
        }
    }
}
