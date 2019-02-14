using System.Collections.Generic;
using System.Text;

namespace FruitWars.Core.Models
{
    public class WarriorSelectGameState : GameState
    {
        private readonly Dictionary<int, int> _warriorTypesByPlayerNumber;
        private readonly StringBuilder _stringBuilder;

        public WarriorSelectGameState()
        {
            _warriorTypesByPlayerNumber = new Dictionary<int, int>();
            _stringBuilder = new StringBuilder();
        }

        public string DisplayMessage => _stringBuilder?.ToString();

        public void AddLineToMessage(string text)
        {
            _stringBuilder.AppendLine(text);
        }
    }
}
