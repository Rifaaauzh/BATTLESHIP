using Battleship.Interfaces;

namespace Battleship.Models;

public class Board : IBoard
{
    public int Size { get; set;}
    public Cell [,] Cells { get; set; }

    public Board(int size)
    {
        Size = size;
        Cells = new Cell[size, size];

        //initialie semua cell nya

        for (int i = 0; i<size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Cells[i, j] = new Cell(new Position(i,j));
            }
        }
    }

    public ICell GetCell(Position position)
    {
        return Cells[position.X, position.Y];
    }
}