using FruitWars.Core;

namespace NETFramework.TestConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            new GameController(null, null, null, null, null).RunGameLoop();
        }
    }
}
