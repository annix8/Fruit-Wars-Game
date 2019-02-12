using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.Models.Fruits
{
    public class Apple : Fruit
    {
        public override void IncreasePointsToWarrior(Warrior warrior)
        {
            warrior.Power++;
        }
    }
}
