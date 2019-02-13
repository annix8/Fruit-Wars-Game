using FruitWars.Core.Models;
using FruitWars.Core.Models.Enums;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;
using System;
using System.Collections.Generic;

namespace FruitWars.Core
{
    public class BoardController
    {
        private readonly Dictionary<int, (int, int)> _warriorPositionsByPlayerNumber;
        private readonly Random _random;

        public BoardController()
        {
            _warriorPositionsByPlayerNumber = new Dictionary<int, (int, int)>();
            _random = new Random();
        }

        public Board Board { get; private set; }

        public void CreateNewBoard(Dictionary<int, int> warriorTypesByPlayerNumber)
        {
            InitializeBoardWithNullBoardObjects();
            AddPlayerWarriorsToBoard(warriorTypesByPlayerNumber);
        }

        public void MovePlayerWarrior(int playerNumber, Direction direction)
        {
            int warriorCurrentRow = _warriorPositionsByPlayerNumber[playerNumber].Item1;
            int warriorCurrentCol = _warriorPositionsByPlayerNumber[playerNumber].Item2;
            int desiredRow = warriorCurrentRow + 1; // todo according to direction
            int desiredCol = warriorCurrentCol + 1; // todo according to direction
            if ((desiredRow < 0 || desiredRow >= Board.Rows)
                || (desiredCol < 0 || desiredCol >= Board.Cols))
            {
                // player is trying to move out of board so return his warrior's current position
                // maybe throw custom exception and catch it in caller?
            }

            Warrior warrior = Board[warriorCurrentRow, warriorCurrentCol] as Warrior;
            if (warrior == null)
            {
                throw new ArgumentException($"Row: {warriorCurrentRow}, Col: {warriorCurrentCol} is not a warrior but a {Board[warriorCurrentRow, warriorCurrentCol].GetType()}");
            }

            // todo think of a way not to use if else "pattern"
            BoardObject boardObject = Board[desiredRow, desiredCol];
            if (boardObject is Fruit)
            {
                warrior.EatFruit((Fruit)boardObject);
                Board[desiredRow, desiredCol] = warrior;
            }
            else if (boardObject is Warrior)
            {

            }
            else
            {
                Board[desiredRow, desiredCol] = warrior;
            }

            Board[warriorCurrentRow, warriorCurrentCol] = null;
        }

        private void InitializeBoardWithNullBoardObjects()
        {
            Board = new Board();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Cols; j++)
                {
                    Board[i, j] = new NullBoardObject();
                }
            }
        }

        private void AddPlayerWarriorsToBoard(Dictionary<int, int> warriorTypesByPlayerNumber)
        {
            int upperQuadrant = _random.Next(0, Board.Rows / 2);
            int lowerQuadrant = _random.Next(Board.Rows / 2, Board.Rows);
            int[] quadrantRows = new int[] { upperQuadrant, lowerQuadrant };
            int leftQuadrant = _random.Next(0, Board.Cols / 2);
            int rightQuadrant = _random.Next(Board.Cols / 2, Board.Cols);
            int[] quadrantCols = new int[] { leftQuadrant, rightQuadrant };
            int row = quadrantRows[_random.Next(0, quadrantRows.Length)];
            int col = quadrantCols[_random.Next(0, quadrantCols.Length)];

            var mockX = 0;
            var mockY = 0;
            foreach (var kvp in warriorTypesByPlayerNumber)
            {
                int playerNumber = kvp.Key;
                int warriorType = kvp.Value;
                _warriorPositionsByPlayerNumber[playerNumber] = (mockX, mockY);
                mockX = 8;
                mockY = 8;
                // randomly put the players' warriors on the board + fruits
            }
        }
    }
}
