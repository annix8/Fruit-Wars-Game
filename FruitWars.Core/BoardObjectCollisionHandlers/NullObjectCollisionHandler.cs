using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers
{
    public class NullObjectCollisionHandler : IBoardObjectCollisionHandler
    {
        private readonly Warrior _currentWarriorOnTurn;
        private int _desiredRow;
        private int _desiredCol;

        public NullObjectCollisionHandler(Warrior currentWarriorOnTurn,
            int desiredRow,
            int desiredCol)
        {
            _currentWarriorOnTurn = currentWarriorOnTurn;
            _desiredRow = desiredRow;
            _desiredCol = desiredCol;
        }

        public void Handle(Board board)
        {
            board[_desiredRow, _desiredCol] = _currentWarriorOnTurn;
        }
    }
}
