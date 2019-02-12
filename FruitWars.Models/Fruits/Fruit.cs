using FruitWars.Models.Warriors;

namespace FruitWars.Models.Fruits
{
    public abstract class Fruit : BoardObject
    {
        public abstract void IncreasePointsToWarrior(Warrior warrior);
    }
}
