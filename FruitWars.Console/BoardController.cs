using FruitWars.Models;
using FruitWars.Models.Fruits;
using FruitWars.Models.Warriors;
using System;

namespace NETFramework.TestConsoleApp
{
    public class BoardController
    {
        private readonly Board _board;
        private readonly Random _random;

        public BoardController(Board board)
        {
            _board = board;
            _random = new Random();
        }

        public void InitializeBoard(params Player[] players)
        {
            foreach (var player in players)
            {

            }
        }

        public (int, int) MoveWarrior(int warriorCurrentRow, int warriorCurrentCol, Direction direction)
        {
            int desiredRow = warriorCurrentRow + 1; // todo according to direction
            int desiredCol = warriorCurrentCol + 1; // todo according to direction
            if ((desiredRow < 0 || desiredRow >= _board.Rows)
                || (desiredCol < 0 || desiredCol >= _board.Cols))
            {
                // player is trying to move out of board so return his warrior's current position
                return (warriorCurrentRow, warriorCurrentCol);
                // maybe throw custom exception and catch it in caller?
            }

            Warrior warrior = _board[warriorCurrentRow, warriorCurrentCol] as Warrior;

            if (warrior == null)
            {
                throw new ArgumentException($"Row: {warriorCurrentRow}, Col: {warriorCurrentCol} is not a warrior but a {_board[warriorCurrentRow, warriorCurrentCol].GetType()}");
            }

            BoardObject boardObject = _board[desiredRow, desiredCol];
            if (boardObject is Fruit)
            {
                warrior.EatFruit((Fruit)boardObject);
                _board[desiredRow, desiredCol] = warrior;
            }
            else if (boardObject is Warrior)
            {

            }
            else
            {
                _board[desiredRow, desiredCol] = warrior;
            }

            _board[warriorCurrentRow, warriorCurrentCol] = null;

            return (desiredRow, desiredCol);
        }
    }
}
