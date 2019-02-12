using FruitWars.Contracts;
using FruitWars.Contracts.IO;
using System;

namespace FruitWars.IO
{
    public class ConsoleRenderer : IRenderer
    {
        public void RenderFrame(IFrame frame)
        {
            Console.Clear();
            Console.WriteLine(frame.Content.ToString());
        }

        public void RenderMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
