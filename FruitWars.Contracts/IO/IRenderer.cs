using FruitWars.Models.Contracts;

namespace FruitWars.Contracts.IO
{
    public interface IRenderer
    {
        void RenderMessage(string message);
        void RenderBoard(IBoard board);
    }
}
