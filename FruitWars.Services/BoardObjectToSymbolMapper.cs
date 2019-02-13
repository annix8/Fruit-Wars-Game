using FruitWars.Core.Models;
using FruitWars.Core.Models.Warriors;
using System;
using System.Collections.Generic;

namespace FruitWars.Services
{
    public class BoardObjectToSymbolMapper
    {
        private Dictionary<BoardObject, Func<char>> _symbolsByBoardObject;

        public BoardObjectToSymbolMapper()
        {
           
        }

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
