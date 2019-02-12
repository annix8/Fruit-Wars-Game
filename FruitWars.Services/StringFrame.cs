using FruitWars.Contracts;

namespace FruitWars.Services
{
    public class StringFrame : IFrame
    {
        public StringFrame(string content)
        {
            Content = content;
        }

        public object Content { get; set; }
    }
}
