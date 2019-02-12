using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.Models.Fruits
{
    public class Pear : Fruit
    {
        public override void IncreasePointsToWarrior(Warrior warrior)
        {
            warrior.Speed++;
        }
    }
}
