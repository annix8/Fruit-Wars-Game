using FruitWars.Contracts.IO;
using System;

namespace FruitWars.IO
{
    public class ConsoleRenderer : IRenderer
    {
        public void RenderMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
