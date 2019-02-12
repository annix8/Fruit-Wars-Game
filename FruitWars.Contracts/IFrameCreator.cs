using FruitWars.Models;
using FruitWars.Models.Contracts;
using System.Collections.Generic;

namespace FruitWars.Contracts
{
    public interface IFrameCreator
    {
        IFrame CreateFrame(Board board, List<Player> players);
    }
}
