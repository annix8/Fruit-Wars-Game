using FruitWars.Core.Models.Enums;

namespace FruitWars.Contracts.IO
{
    public interface IInputReceiver
    {
        string ReceiveStringInput();
        Direction ReceiveDirectionInput();
    }
}
