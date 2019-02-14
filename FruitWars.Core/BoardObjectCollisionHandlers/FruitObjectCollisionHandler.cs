using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers
{
    public class FruitObjectCollisionHandler : IBoardObjectCollisionHandler
    {
        private readonly GameStateController _gameStateController;
        private readonly Warrior _currentWarriorOnTurn;
        private readonly Fruit _fruit;
        private int _desiredRow;
        private int _desiredCol;
        private int _playerNumberOnTurn;

        public FruitObjectCollisionHandler(GameStateController gameStateController,
            Warrior currentWarriorOnTurn,
            Fruit fruit,
            int desiredRow,
            int desiredCol,
            int playerNumberOnTurn)
        {
            _gameStateController = gameStateController;
            _currentWarriorOnTurn = currentWarriorOnTurn;
            _fruit = fruit;
            _desiredRow = desiredRow;
            _desiredCol = desiredCol;
            _playerNumberOnTurn = playerNumberOnTurn;
        }

        public void Handle(Board board)
        {
            _currentWarriorOnTurn.EatFruit(_fruit);
            board[_desiredRow, _desiredCol] = _currentWarriorOnTurn;
            _gameStateController.AssignWarriorPositionToPlayer(_playerNumberOnTurn, _desiredRow, _desiredCol);
        }
    }
}
