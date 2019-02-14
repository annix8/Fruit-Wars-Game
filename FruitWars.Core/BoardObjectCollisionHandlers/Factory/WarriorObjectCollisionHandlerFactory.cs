using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Controllers;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers.Factory
{
    public class WarriorObjectCollisionHandlerFactory : IObjectCollisionHandlerFactory
    {
        private readonly GameStateController _gameStateController;

        public WarriorObjectCollisionHandlerFactory(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }

        public IBoardObjectCollisionHandler Create(Warrior currentWarriorOnTurn, BoardObject boardObject,
            int desiredRow, int desiredCol, int playerNumberOnTurn)
        {
            return new WarriorObjectCollisionHandler(_gameStateController, currentWarriorOnTurn, (Warrior)boardObject,
                    desiredRow, desiredCol, playerNumberOnTurn);
        }

        public bool ShouldCreate(BoardObject boardObject)
        {
            return boardObject is Warrior;
        }
    }
}
