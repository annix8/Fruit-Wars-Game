using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Controllers;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers.Factory
{
    public class NullObjectCollisionHandlerFactory : IObjectCollisionHandlerFactory
    {
        private readonly GameStateController _gameStateController;

        public NullObjectCollisionHandlerFactory(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }

        public IBoardObjectCollisionHandler Create(Warrior currentWarriorOnTurn, BoardObject boardObject,
            int desiredRow, int desiredCol, int playerNumberOnTurn)
        {
            return new NullObjectCollisionHandler(_gameStateController, currentWarriorOnTurn,
                desiredRow, desiredCol, playerNumberOnTurn);
        }

        public bool ShouldCreate(BoardObject boardObject)
        {
            return boardObject is NullBoardObject;
        }
    }
}
