namespace FruitWars.Contracts.IO
{
    public interface IRenderer
    {
        void RenderMessage(string message);
        void RenderFrame(IFrame frame);
    }
}
