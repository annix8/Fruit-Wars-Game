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
        private readonly Random _random;

        public BoardController()
        {
            InitializeBoard();
            _random = new Random();
        }

        public Board Board { get; private set; }

        public void AddPlayersWarriorsToBoard(List<Player> players)
        {
            foreach (var player in players)
            {
                // randomly put the players' warriors on the board + fruits
            }
        }

        public (int, int) MoveWarrior(int warriorCurrentRow, int warriorCurrentCol, Direction direction)
        {
            int desiredRow = warriorCurrentRow + 1; // todo according to direction
            int desiredCol = warriorCurrentCol + 1; // todo according to direction
            if ((desiredRow < 0 || desiredRow >= Board.Rows)
                || (desiredCol < 0 || desiredCol >= Board.Cols))
            {
                // player is trying to move out of board so return his warrior's current position
                return (warriorCurrentRow, warriorCurrentCol);
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

            return (desiredRow, desiredCol);
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Cols; j++)
                {
                    Board[i, j] = new NullBoardObject();
                }
            }
        }
    }
}
