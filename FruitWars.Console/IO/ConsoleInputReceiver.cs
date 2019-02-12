using FruitWars.Contracts.IO;
using System;

namespace FruitWars.IO
{
    public class ConsolePlayerInputReceiver : IInputReceiver
    {
        public string ReceiveStringInput()
        {
            return Console.ReadLine();
        }
    }
}
