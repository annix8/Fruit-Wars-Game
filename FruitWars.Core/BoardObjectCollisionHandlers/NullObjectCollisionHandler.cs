using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers
{
    public class NullObjectCollisionHandler : IBoardObjectCollisionHandler
    {
        private readonly GameStateController _gameStateController;
        private readonly Warrior _currentWarriorOnTurn;
        private int _desiredRow;
        private int _desiredCol;
        private int _playerNumberOnTurn;

        public NullObjectCollisionHandler(GameStateController gameStateController,
            Warrior currentWarriorOnTurn,
            int desiredRow,
            int desiredCol,
            int playerNumberOnTurn)
        {
            _gameStateController = gameStateController;
            _currentWarriorOnTurn = currentWarriorOnTurn;
            _desiredRow = desiredRow;
            _desiredCol = desiredCol;
            _playerNumberOnTurn = playerNumberOnTurn;
        }

        public void Handle(Board board)
        {
            board[_desiredRow, _desiredCol] = _currentWarriorOnTurn;
            _gameStateController.AssignWarriorPositionToPlayer(_playerNumberOnTurn, _desiredRow, _desiredCol);
        }
    }
}
