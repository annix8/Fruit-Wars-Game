using FruitWars.Models.Warriors;

namespace FruitWars.Models.Fruits
{
    public class Pear : Fruit
    {
        public override void IncreasePointsToWarrior(Warrior warrior)
        {
            warrior.Speed++;
        }
    }
}
