using FruitWars.Core.Models;

namespace FruitWars.Services
{
    public class BoardObjectToSymbolMapper
    {
        public char GetSymbol(BoardObject boardObject)
        {
            if(boardObject is NullBoardObject)
            {
                return '-';
            }

            return 'O';
        }
    }
}
