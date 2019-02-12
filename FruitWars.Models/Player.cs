using FruitWars.Models.Warriors;

namespace FruitWars.Models
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
            return $"Player{Number}: {Warrior.Power} Power; {Warrior.Speed} Speed";
        }
    }
}
