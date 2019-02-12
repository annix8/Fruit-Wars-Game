using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.Models.Fruits
{
    public abstract class Fruit : BoardObject
    {
        public abstract void IncreasePointsToWarrior(Warrior warrior);
    }
}
