using FruitWars.Models.Warriors;

namespace FruitWars.Models.Fruits
{
    public class Apple : Fruit
    {
        public override void IncreasePointsToWarrior(Warrior warrior)
        {
            warrior.Power++;
        }
    }
}
