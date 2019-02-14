using FruitWars.Core.Models;

namespace FruitWars.Core.BoardObjectCollisionHandlers.Contracts
{
    public interface IBoardObjectCollisionHandler
    {
        void Handle(Board board);
    }
}
