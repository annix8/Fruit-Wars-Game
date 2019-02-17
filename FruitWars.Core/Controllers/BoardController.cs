using FruitWars.Core.BoardObjectCollisionHandlers.Contracts;
using FruitWars.Core.BoardObjectCollisionHandlers.Factory;
using FruitWars.Core.Factory;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Enums;
using FruitWars.Core.Models.Fruits;
using FruitWars.Core.Models.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitWars.Core.Controllers
{
    public class BoardController
    {
        private const int WarriorsOffset = 2;
        private const int FruitsOffset = 1;

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

        public void CreateNewBoard(Dictionary<int, Warrior> warriorsByPlayerNumber)
        {
            CreateBoardWithNullBoardObjects();
            PlaceWarriors(warriorsByPlayerNumber);
            PlaceFruits();
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

        private void PlaceWarriors(Dictionary<int, Warrior> warriorsByPlayerNumber)
        {
            HashSet<(int, int)> validCells = GetCellsWithNullBoardObjects();
            foreach (var kvp in warriorsByPlayerNumber)
            {
                int playerNumber = kvp.Key;
                Warrior warrior = kvp.Value;

                (int row, int col) = GetPlaceableRowCol(validCells, WarriorsOffset);
                _gameStateController.AssignWarriorPositionToPlayer(playerNumber, row, col);
                Board[row, col] = warrior;
            }
        }

        private void PlaceFruits()
        {
            HashSet<(int, int)> validCells = GetCellsWithNullBoardObjects();
            List<Fruit> fruits = _fruitFactory.Create();
            foreach (var fruit in fruits)
            {
                (int row, int col) = GetPlaceableRowCol(validCells, FruitsOffset);
                Board[row, col] = fruit;
            }
        }

        private HashSet<(int, int)> GetCellsWithNullBoardObjects()
        {
            HashSet<(int, int)> validCells = new HashSet<(int, int)>();
            for (int row = 0; row < Board.Rows; row++)
            {
                for (int col = 0; col < Board.Cols; col++)
                {
                    if (Board[row, col] is NullBoardObject)
                    {
                        validCells.Add((row, col));
                    }
                }
            }

            return validCells;
        }

        private (int, int) GetPlaceableRowCol(HashSet<(int, int)> validCells, int cellsOffset)
        {
            (int placeableRow, int placeableCol) = validCells.ElementAt(_random.Next(validCells.Count));
            (int minRow, int maxRow) = GetMinAndMaxDimension(placeableRow, Board.Rows - 1, cellsOffset);
            int colOffset = 0;
            int row = minRow;
            int minCol = placeableRow;
            int maxCol = placeableCol;
            while (row <= maxRow)
            {
                (minCol, maxCol) = GetMinAndMaxDimension(placeableCol, Board.Cols - 1, colOffset);
                for (int col = minCol; col <= maxCol; col++)
                {
                    validCells.Remove((row, col));
                }

                if (row < placeableRow)
                {
                    colOffset++;
                }
                else
                {
                    colOffset--;
                }

                row++;
            }

            return (placeableRow, placeableCol);
        }

        private (int, int) GetMinAndMaxDimension(int dimension, int maxAllowed,  int offset)
        {
            int minDimension = Math.Max(dimension - offset, 0);
            int maxDimension = Math.Min(dimension + offset, maxAllowed);

            return (minDimension, maxDimension);
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
