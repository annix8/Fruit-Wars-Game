using FruitWars.Core.Models.GameState;

namespace FruitWars.Contracts
{
    public interface IFrameCreator
    {
        IFrame CreateFrame(GameStateBase gameState);
    }
}
