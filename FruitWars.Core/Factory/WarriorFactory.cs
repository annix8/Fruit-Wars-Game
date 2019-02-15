using FruitWars.Core.Models.Warriors;
using System;
using System.Collections.Generic;

namespace FruitWars.Core.Factory
{
    public class WarriorFactory
    {
        private readonly IDictionary<int, Func<Warrior>> _createWarriorFunctions;

        public WarriorFactory()
        {
            _createWarriorFunctions = new Dictionary<int, Func<Warrior>>
            {
                [1] = () => new Turtle(),
                [2] = () => new Monkey(),
                [3] = () => new Pigeon()
            };
        }

        public Warrior Create(int warriorType)
        {
            if (!_createWarriorFunctions.ContainsKey(warriorType))
            {
                return null;
            }

            return _createWarriorFunctions[warriorType].Invoke();
        }
    }
}
