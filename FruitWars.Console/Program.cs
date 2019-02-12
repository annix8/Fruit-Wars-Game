using FruitWars.Core;
using FruitWars.IO;
using FruitWars.Models;

namespace NETFramework.TestConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            new GameController(null, null, null, null).RunGameLoop();
        }
    }
}
