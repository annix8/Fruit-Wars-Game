﻿using FruitWars.Core.Models;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;
using System;
using System.Collections.Generic;

namespace FruitWars.Services
{
    public class BoardObjectToSymbolMapper
    {
        private Dictionary<Type, Func<string>> _symbolsByBoardObject;

        public BoardObjectToSymbolMapper()
        {
            _symbolsByBoardObject = new Dictionary<Type, Func<string>>
            {
                { typeof(NullBoardObject), () => "-" },
                { typeof(Apple), () => "A" },
                { typeof(Pear), () => "P" },
            };
        }

        public string GetSymbol(BoardObject boardObject, int playerNumber)
        {
            if (!_symbolsByBoardObject.ContainsKey(boardObject.GetType()))
            {
                throw new ArgumentException($"Board object not found.");
            }

            return _symbolsByBoardObject[boardObject.GetType()].Invoke();
        }
    }
}
