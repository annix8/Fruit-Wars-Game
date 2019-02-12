using FruitWars.Core;

namespace FruitWars
{
    public class Program
    {
        static void Main(string[] args)
        {
            new GameController(null, null, null, null, null).RunGameLoop();
        }
    }
}
