using FruitWars.Core.Models;
using System;
using System.Collections.Generic;

namespace FruitWars.Core.Factory
{
    public class PlayerFactory
    {
        private const int MinimalPlayerCount = 2;

        public List<Player> Create(int playerCount = MinimalPlayerCount)
        {
            if(playerCount < MinimalPlayerCount)
            {
                throw new ArgumentException($"At least {MinimalPlayerCount} playes must be created.");
            }

            List<Player> players = new List<Player>();
            for (int playerNumber = 1; playerNumber <= playerCount; playerNumber++)
            {
                var player = new Player(playerNumber);
                players.Add(player);
            }

            return players;
        }
    }
}
