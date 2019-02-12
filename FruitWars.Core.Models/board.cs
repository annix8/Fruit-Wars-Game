namespace FruitWars.Core.Models
{
    public class Board
    {
        private readonly BoardObject[,] _boardObjects;

        public Board(int rows = 8, int cols = 8)
        {
            Rows = rows;
            Cols = cols;

            _boardObjects = new BoardObject[rows, cols];
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
    }
}
