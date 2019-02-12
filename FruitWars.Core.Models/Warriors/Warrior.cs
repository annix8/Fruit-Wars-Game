using FruitWars.Core.Models.Fruits;

namespace FruitWars.Core.Models.Warriors
{
    public abstract class Warrior : BoardObject
    {
        public int Speed { get; set; }
        public int Power { get; set; }

        public void EatFruit(Fruit fruit)
        {
            fruit.IncreasePointsToWarrior(this);
        }

        public override string ToString()
        {
            return $"{GetType().Name} with Power: {Power}, Speed: {Speed}";
        }
    }
}
