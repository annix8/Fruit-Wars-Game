using FruitWars.Core.Factory;
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
        private readonly GameStateController _gameStateController;
        private readonly FruitFactory _fruitFactory;
        private readonly Random _random;

        public BoardController(GameStateController gameStateController,
            FruitFactory fruitFactory)
        {
            _gameStateController = gameStateController;
            _fruitFactory = fruitFactory;
            _random = new Random();
        }

        public Board Board { get; private set; }

        public void CreateNewBoard(Dictionary<int, Warrior> warriorTypesByPlayerNumber)
        {
            InitializeBoardWithNullBoardObjects();
            AddWarriorsAndFruitsToBoard(warriorTypesByPlayerNumber);
            _gameStateController.GameState.Board = Board;
        }

        public void MovePlayerWarrior(int playerNumber, Direction direction)
        {
            // todo fix logic that checks the winner
            (int warriorCurrentRow, int warriorCurrentCol) = _gameStateController.GetWarriorPositionsByPlayerNumber(playerNumber);
            (int desiredRow, int desiredCol) = GetNextPosition(warriorCurrentRow, warriorCurrentCol, direction);
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
            else if (boardObject is Warrior otherWarrior)
            {
                if (warrior.Power > otherWarrior.Power)
                {
                    // player that made the move wins
                    Board[desiredRow, desiredCol] = warrior;
                    _gameStateController.EndGameWithWinner(playerNumber);
                }
                else if (warrior.Power < otherWarrior.Power)
                {
                    // player that has warrior on desiredRow, desiredCol wins
                    int otherPlayerNumber = _gameStateController.GetPlayerNumberByWarriorPosition(desiredRow, desiredCol);
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
            _gameStateController.AssignWarriorPositionToPlayer(playerNumber, desiredRow, desiredCol);
        }

        private void InitializeBoardWithNullBoardObjects()
        {
            Board = new Board();
            for (int row = 0; row < Board.Rows; row++)
            {
                for (int col = 0; col < Board.Cols; col++)
                {
                    Board[row, col] = new NullBoardObject();
                }
            }
        }

        private void AddWarriorsAndFruitsToBoard(Dictionary<int, Warrior> warriorsByPlayerNumber)
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
                    if (!takenPositions.Contains((randomRow, randomCol)))
                    {
                        break;
                    }
                }

                _gameStateController.AssignWarriorPositionToPlayer(playerNumber, randomRow, randomCol);
                Board[randomRow, randomCol] = warrior;
                takenPositions.Add((randomRow, randomCol));
            }

            List<Fruit> fruits = _fruitFactory.Create();
            foreach (var fruit in fruits)
            {
                while (true)
                {
                    randomRow = _random.Next(0, Board.Rows);
                    randomCol = _random.Next(0, Board.Cols);

                    // todo at least 3 positions away logic!
                    if (!takenPositions.Contains((randomRow, randomCol)))
                    {
                        break;
                    }
                }

                Board[randomRow, randomCol] = fruit;
                takenPositions.Add((randomRow, randomCol));
            }
        }

        private (int, int) GetNextPosition(int currentRow, int currentCol, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return (currentRow - 1, currentCol);
                case Direction.Down:
                    return (currentRow + 1, currentCol);
                case Direction.Left:
                    return (currentRow, currentCol - 1);
                case Direction.Right:
                    return (currentRow, currentCol + 1);
                default:
                    throw new ArgumentException("No such direction");
            }
        }
    }
}
