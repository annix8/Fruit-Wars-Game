using FruitWars.Contracts.IO;
using System;

namespace NETFramework.TestConsoleApp.IO
{
    public class ConsolePlayerInputReceiver : IInputReceiver
    {
        public string ReceiveStringInput()
        {
            return Console.ReadLine();
        }
    }
}
