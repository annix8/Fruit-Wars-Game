using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core.Models
{
    public class Player
    {
        public Player(int playerNumber)
        {
            Number = playerNumber;
        }

        public Warrior Warrior { get; set; }
        public int Number { get; private set; }

        public override string ToString()
        {
            return $"Player{Number} - {Warrior.GetType().Name}: {Warrior.Power} Power; {Warrior.Speed} Speed";
        }
    }
}
