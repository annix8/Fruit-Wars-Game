using System.Collections.Generic;
using FruitWars.Core.Models.Fruits;

namespace FruitWars.Core.Factory
{
    public class FruitFactory
    {
        private const int NumberOfApples = 4;
        private const int NumberOfPears = 3;

        public List<Fruit> Create(int numberOfApples = NumberOfApples, int numberOfPears = NumberOfPears)
        {
            List<Fruit> fruits = new List<Fruit>();
            fruits.AddRange(CreateApples(numberOfApples));
            fruits.AddRange(CreatePears(numberOfPears));

            return fruits;
        }

        private List<Apple> CreateApples(int numberOfApples)
        {
            List<Apple> apples = new List<Apple>();
            for (int i = 0; i < numberOfApples; i++)
            {
                apples.Add(new Apple());
            }

            return apples;
        }

        private List<Pear> CreatePears(int numberOfPears)
        {
            List<Pear> pears = new List<Pear>();
            for (int i = 0; i < numberOfPears; i++)
            {
                pears.Add(new Pear());
            }

            return pears;
        }
    }
}
