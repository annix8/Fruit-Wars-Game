using FruitWars.Contracts;
using FruitWars.Contracts.IO;
using FruitWars.Core;
using FruitWars.IO;
using FruitWars.Services;

namespace FruitWars
{
    public class Program
    {
        static void Main(string[] args)
        {
            IInputReceiver inputReceiver = new ConsoleInputReceiver();
            IRenderer renderer = new ConsoleRenderer();
            IFrameCreator frameCreator = new StringFrameCreator(new BoardObjectToSymbolMapper());
            new GameEngine(inputReceiver, renderer, frameCreator).RunGame();
        }
    }
}
