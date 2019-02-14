using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Controllers;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers.Factory
{
    public class FruitObjectCollisionHandlerFactory : IObjectCollisionHandlerFactory
    {
        private readonly GameStateController _gameStateController;

        public FruitObjectCollisionHandlerFactory(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }

        public IBoardObjectCollisionHandler Create(Warrior currentWarriorOnTurn, BoardObject boardObject,
            int desiredRow, int desiredCol, int playerNumberOnTurn)
        {
            return new FruitObjectCollisionHandler(_gameStateController, currentWarriorOnTurn, (Fruit)boardObject,
                    desiredRow, desiredCol, playerNumberOnTurn);
        }

        public bool ShouldCreate(BoardObject boardObject)
        {
            return boardObject is Fruit;
        }
    }
}
