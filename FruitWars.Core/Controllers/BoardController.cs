using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.BoardObjectCollisionHandlers.Factory;
using FruitWars.Core.Factory;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Enums;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;
using System;
using System.Collections.Generic;

namespace FruitWars.Core.Controllers
{
    public class BoardController
    {
        private readonly GameStateController _gameStateController;
        private readonly FruitFactory _fruitFactory;
        private readonly AbstractObjectCollisionHandlerFactory _objectCollisionHandlerFactory;
        private readonly Random _random;

        public BoardController(GameStateController gameStateController,
            FruitFactory fruitFactory,
            AbstractObjectCollisionHandlerFactory objectCollisionHandlerFactory)
        {
            _gameStateController = gameStateController;
            _fruitFactory = fruitFactory;
            _objectCollisionHandlerFactory = objectCollisionHandlerFactory;

            _random = new Random();
        }

        public Board Board { get; private set; }

        public void CreateNewBoard(Dictionary<int, Warrior> warriorTypesByPlayerNumber)
        {
            CreateBoardWithNullBoardObjects();
            PlaceWarriorsAndFruitsOnBoard(warriorTypesByPlayerNumber);
            _gameStateController.AddBoardToGameState(Board);
        }

        public bool MovePlayerWarrior(int playerNumber, Direction direction)
        {
            (int warriorCurrentRow, int warriorCurrentCol) = _gameStateController.GetWarriorPositionsByPlayerNumber(playerNumber);
            (int desiredRow, int desiredCol) = GetDesiredPosition(warriorCurrentRow, warriorCurrentCol, direction);
            if ((desiredRow < 0 || desiredRow >= Board.Rows)
                || (desiredCol < 0 || desiredCol >= Board.Cols))
            {
                // player is trying to move out of board which is not allowed
                return false;
            }

            Warrior warrior = Board[warriorCurrentRow, warriorCurrentCol] as Warrior;
            if (warrior == null)
            {
                throw new ArgumentException($"Row: {warriorCurrentRow}, Col: {warriorCurrentCol} is not a warrior but a {Board[warriorCurrentRow, warriorCurrentCol].GetType()}");
            }

            BoardObject boardObject = Board[desiredRow, desiredCol];
            IBoardObjectCollisionHandler boardObjectCollisionHandler = _objectCollisionHandlerFactory
                .Create(warrior, boardObject, desiredRow, desiredCol, playerNumber);
            boardObjectCollisionHandler.Handle(Board);

            Board[warriorCurrentRow, warriorCurrentCol] = null;

            return true;
        }

        private void CreateBoardWithNullBoardObjects()
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

        private void PlaceWarriorsAndFruitsOnBoard(Dictionary<int, Warrior> warriorsByPlayerNumber)
        {
            List<(int, int)> takenPositions = new List<(int, int)>();
            PlaceWarriors(warriorsByPlayerNumber, takenPositions);
            PlaceFruits(takenPositions);
        }

        private void PlaceWarriors(Dictionary<int, Warrior> warriorsByPlayerNumber, List<(int, int)> takenPositions)
        {
            int randomRow = _random.Next(0, Board.Rows);
            int randomCol = _random.Next(0, Board.Cols);
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
        }

        private void PlaceFruits(List<(int, int)> takenPositions)
        {
            int randomRow = _random.Next(0, Board.Rows);
            int randomCol = _random.Next(0, Board.Cols);
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

        private (int, int) GetDesiredPosition(int currentRow, int currentCol, Direction direction)
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
                case Direction.None:
                    return (-1, -1);
                default:
                    throw new ArgumentException("No such direction");
            }
        }
    }
}
