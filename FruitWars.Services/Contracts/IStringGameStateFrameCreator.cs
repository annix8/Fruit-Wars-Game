using FruitWars.Contracts;
using FruitWars.Core.Models.GameState;

namespace FruitWars.Services.Contracts
{
    public interface IStringGameStateFrameCreator
    {
        IFrame Create(GameStateBase gameState);
        bool ShouldCreate(GameStateBase gameState);
    }
}
