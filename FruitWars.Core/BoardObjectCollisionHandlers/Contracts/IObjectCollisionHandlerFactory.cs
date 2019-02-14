using FruitWars.Core.Models;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.BoardObjectCollisionHandlers.Contracts
{
    public interface IObjectCollisionHandlerFactory
    {
        IBoardObjectCollisionHandler Create(Warrior currentWarriorOnTurn, BoardObject boardObject,
            int desiredRow, int desiredCol, int playerNumberOnTurn);
        bool ShouldCreate(BoardObject boardObject);
    }
}
