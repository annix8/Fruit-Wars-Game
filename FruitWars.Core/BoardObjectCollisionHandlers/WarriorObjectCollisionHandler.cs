using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers
{
    public class WarriorObjectCollisionHandler : IBoardObjectCollisionHandler
    {
        private readonly GameStateController _gameStateController;
        private readonly Warrior _currentWarriorOnTurn;
        private readonly Warrior _otherWarrior;
        private int _desiredRow;
        private int _desiredCol;
        private int _playerNumberOnTurn;


        public WarriorObjectCollisionHandler(GameStateController gameStateController,
            Warrior currentWarriorOnTurn,
            Warrior otherWarrior,
            int desiredRow,
            int desiredCol,
            int playerNumberOnTurn)
        {
            _gameStateController = gameStateController;
            _currentWarriorOnTurn = currentWarriorOnTurn;
            _otherWarrior = otherWarrior;
            _desiredRow = desiredRow;
            _desiredCol = desiredCol;
            _playerNumberOnTurn = playerNumberOnTurn;
        }

        public void Handle(Board board)
        {
            if (_currentWarriorOnTurn.Power > _otherWarrior.Power)
            {
                // player that made the move wins
                board[_desiredRow, _desiredCol] = _currentWarriorOnTurn;
                _gameStateController.EndGameWithWinner(_playerNumberOnTurn);
            }
            else if (_currentWarriorOnTurn.Power < _otherWarrior.Power)
            {
                // player that has warrior on desiredRow, desiredCol wins
                int otherPlayerNumber = _gameStateController.GetPlayerNumberByWarriorPosition(_desiredRow, _desiredCol);
                _gameStateController.EndGameWithWinner(otherPlayerNumber);
            }
            else
            {
                _gameStateController.EndGameWithDraw();
            }
        }
    }
}
