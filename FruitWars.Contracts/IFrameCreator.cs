using FruitWars.Core.Models;

namespace FruitWars.Contracts
{
    public interface IFrameCreator
    {
        IFrame CreateFrame(GameState gameState);
    }
}
