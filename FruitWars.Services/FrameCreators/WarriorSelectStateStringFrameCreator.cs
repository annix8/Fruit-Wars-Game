using FruitWars.Contracts;
using FruitWars.Core.Models.GameState;
using FruitWars.Services.Contracts;

namespace FruitWars.Services.FrameCreators
{
    public class WarriorSelectStateStringFrameCreator : IStringGameStateFrameCreator
    {
        public IFrame Create(GameStateBase gameState)
        {
            return new StringFrame((gameState as WarriorSelectGameState).DisplayMessage.Trim());
        }

        public bool ShouldCreate(GameStateBase gameState)
        {
            return gameState is WarriorSelectGameState;
        }
    }
}
