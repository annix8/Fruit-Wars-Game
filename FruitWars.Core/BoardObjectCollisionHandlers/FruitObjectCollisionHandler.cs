using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers
{
    public class FruitObjectCollisionHandler : IBoardObjectCollisionHandler
    {
        private readonly Warrior _currentWarriorOnTurn;
        private readonly Fruit _fruit;
        private int _desiredRow;
        private int _desiredCol;

        public FruitObjectCollisionHandler(Warrior currentWarriorOnTurn,
            Fruit fruit,
            int desiredRow,
            int desiredCol)
        {
            _currentWarriorOnTurn = currentWarriorOnTurn;
            _fruit = fruit;
            _desiredRow = desiredRow;
            _desiredCol = desiredCol;
        }

        public void Handle(Board board)
        {
            _currentWarriorOnTurn.EatFruit(_fruit);
            board[_desiredRow, _desiredCol] = _currentWarriorOnTurn;
        }
    }
}
