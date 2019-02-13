using FruitWars.Core.Models;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Services
{
    public class BoardObjectToSymbolMapper
    {
        public char GetSymbol(BoardObject boardObject, int playerNumber)
        {
            if(boardObject is NullBoardObject)
            {
                return '-';
            }
            else if(boardObject is Warrior)
            {

            }

            return 'O';
        }
    }
}
