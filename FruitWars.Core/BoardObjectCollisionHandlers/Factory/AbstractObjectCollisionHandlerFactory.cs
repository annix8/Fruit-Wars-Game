using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Controllers;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;
using System;
using System.Collections.Generic;

namespace FruitWars.Core.BoardObjectCollisionHandlers.Factory
{
    public class AbstractObjectCollisionHandlerFactory
    {
        private readonly List<IObjectCollisionHandlerFactory> _objectCollisionHandlerFactories;

        public AbstractObjectCollisionHandlerFactory(GameStateController gameStateController)
        {
            _objectCollisionHandlerFactories = new List<IObjectCollisionHandlerFactory>
            {
                new FruitObjectCollisionHandlerFactory(gameStateController),
                new WarriorObjectCollisionHandlerFactory(gameStateController),
                new NullObjectCollisionHandlerFactory(gameStateController)
            };
        }

        public IBoardObjectCollisionHandler Create(Warrior currentWarriorOnTurn, BoardObject boardObject,
            int desiredRow, int desiredCol, int playerNumberOnTurn)
        {
            foreach (var factory in _objectCollisionHandlerFactories)
            {
                if (factory.ShouldCreate(boardObject))
                {
                    return factory.Create(currentWarriorOnTurn, boardObject,
                        desiredRow, desiredCol, playerNumberOnTurn);
                }
            }

            throw new ArgumentException($"Board object {boardObject.GetType().Name} does not have collision handler.");
        }
    }
}