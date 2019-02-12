using FruitWars.Core.Models;
using System.Collections.Generic;

namespace FruitWars.Contracts
{
    public interface IFrameCreator
    {
        IFrame CreateFrame(Board board, List<Player> players);
    }
}
