using FruitWars.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.GameState;
using FruitWars.Core.Models.Warriors;
using FruitWars.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FruitWars.Services
{
    public class StringFrameCreator : IFrameCreator
    {
        private readonly List<IStringGameStateFrameCreator> _stringGameStateFrameCreators;

        public StringFrameCreator(BoardObjectToSymbolMapper boardObjectToSymbolMapper)
        {
            _stringGameStateFrameCreators = new List<IStringGameStateFrameCreator>
            {
                new WarriorSelectStateStringFrameCreator(),
                new InProgressStateStringFrameCreator(boardObjectToSymbolMapper),
                new FinishedGameStateStringFrameCreator(boardObjectToSymbolMapper)
            };
        }

        public IFrame CreateFrame(GameStateBase gameState)
        {
            foreach (var frameCreator in _stringGameStateFrameCreators)
            {
                if (frameCreator.ShouldCreate(gameState))
                {
                    return frameCreator.Create(gameState);
                }
            }

            throw new ArgumentException($"Game state {gameState.GetType().Name} does not have an associated string frame creator.");
        }
    }
}
