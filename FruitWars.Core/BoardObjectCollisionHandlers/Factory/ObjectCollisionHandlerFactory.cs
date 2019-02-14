using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Controllers;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers.Factory
{
    public class ObjectCollisionHandlerFactory
    {
        private readonly GameStateController _gameStateController;

        public ObjectCollisionHandlerFactory(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }

        public IBoardObjectCollisionHandler Create(Warrior currentWarriorOnTurn, BoardObject boardObject,
            int desiredRow, int desiredCol, int playerNumberOnTurn)
        {
            // TODO: there has to be a mapping somewhere... is there a better way?
            if (boardObject is Fruit)
            {
                return new FruitObjectCollisionHandler(_gameStateController, currentWarriorOnTurn, (Fruit)boardObject,
                    desiredRow, desiredCol, playerNumberOnTurn);
            }
            else if (boardObject is Warrior)
            {
                return new WarriorObjectCollisionHandler(_gameStateController, currentWarriorOnTurn, (Warrior)boardObject,
                    desiredRow, desiredCol, playerNumberOnTurn);
            }
            else // NullBoardObject
            {
                return new NullObjectCollisionHandler(_gameStateController, currentWarriorOnTurn,
                    desiredRow, desiredCol, playerNumberOnTurn);
            }
        }
    }
}