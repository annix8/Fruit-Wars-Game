using FruitWars.Models.Contracts;

namespace FruitWars.Models
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
