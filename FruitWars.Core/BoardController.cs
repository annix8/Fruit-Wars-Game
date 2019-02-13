using FruitWars.Core.Models;
using FruitWars.Core.Models.Enums;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitWars.Core
{
    public class BoardController
    {
        private readonly GameStateController _gameStateController;
        private readonly Random _random;

        private readonly Dictionary<int, (int, int)> _warriorPositionsByPlayerNumber;

        public BoardController(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            _random = new Random();

            _warriorPositionsByPlayerNumber = new Dictionary<int, (int, int)>();
        }

        public Board Board { get; private set; }

        public void CreateNewBoard(Dictionary<int, Warrior> warriorTypesByPlayerNumber)
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
                // TODO: change of speed happens on the next turn!
                warrior.EatFruit((Fruit)boardObject);
                Board[desiredRow, desiredCol] = warrior;
            }
            else if (boardObject is Warrior otherWarrior)
            {
                if(warrior.Power > otherWarrior.Power)
                {
                    // player that made the move wins
                    _gameStateController.EndGameWithWinner(playerNumber);
                    Board[desiredRow, desiredCol] = warrior;
                }
                else if(warrior.Power < otherWarrior.Power)
                {
                    // player that has warrior on desiredRow, desiredCol wins
                    int otherPlayerNumber = _warriorPositionsByPlayerNumber.First(x => x.Value.Item1 == desiredRow && x.Value.Item2 == desiredCol).Key;
                    _gameStateController.EndGameWithWinner(otherPlayerNumber);
                }
                else
                {
                    _gameStateController.EndGameWithDraw();
                }
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

        private void AddPlayerWarriorsToBoard(Dictionary<int, Warrior> warriorsByPlayerNumber)
        {
            int randomRow = _random.Next(0, Board.Rows);
            int randomCol = _random.Next(0, Board.Cols);
            List<(int, int)> takenPositions = new List<(int, int)>();

            foreach (var kvp in warriorsByPlayerNumber)
            {
                int playerNumber = kvp.Key;
                Warrior warrior = kvp.Value;

                while (true)
                {
                    randomRow = _random.Next(0, Board.Rows);
                    randomCol = _random.Next(0, Board.Cols);

                    // todo at least 3 positions away logic!
                    if(!takenPositions.Contains((randomRow, randomCol)))
                    {
                        break;
                    }
                }

                _warriorPositionsByPlayerNumber[playerNumber] = (randomRow, randomCol);
                Board[randomRow, randomCol] = warrior;
                takenPositions.Add((randomRow, randomCol));
            }
        }
    }
}
