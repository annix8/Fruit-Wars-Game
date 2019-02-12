using FruitWars.Models.Contracts;
using System.Collections;
using System.Collections.Generic;

namespace FruitWars.Models
{
    public class Board : IBoard
    {
        private BoardObject[,] _boardObjects;

        public Board(int rows = 8, int cols = 8)
        {
            Rows = rows;
            Cols = cols;
            
            InitializeBoardWithNullBoardObjects();
        }

        public BoardObject this[int x, int y]
        {
            get => _boardObjects[x, y];
            set
            {
                if (value == null)
                {
                    _boardObjects[x, y] = new NullBoardObject();
                }
                else
                {
                    _boardObjects[x, y] = value;
                }
            }
        }

        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public BoardObject[,] GetBoardField()
        {
            // TODO think of a way to return readonly collection or a new reference so that they cannot be edited from the outside
            return _boardObjects;
        }

        private void InitializeBoardWithNullBoardObjects()
        {
            _boardObjects = new BoardObject[Rows, Cols];

            for (int i = 0; i < _boardObjects.GetLength(0); i++)
            {
                for (int j = 0; j < _boardObjects.GetLength(1); j++)
                {
                    _boardObjects[i, j] = new NullBoardObject();
                }
            }
        }
    }
}
