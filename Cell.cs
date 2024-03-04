using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife
{
  public class Cell
  {
    public int Row { get; private set; }
    public int Col { get; private set; }

    public List<(int row, int col)> Neighbours = new();
    // public int NumOfNeighbours = 0;
    public bool IsAlive = true;

    public Cell(int row, int col, bool alive)
    {
      Row = row;
      Col = col;
      IsAlive = alive;
      GenerateNeighbours();
    }
   public override string ToString()
    {
      return IsAlive ? "*" : " ";
    }

    public void GenerateNeighbours()
    {
      int left = Col - 1; int right = Col + 1;
      int up = Row - 1; int down = Row + 1;

      bool canGoLeft = left >= 0; bool canGoRight = right < Game.Size;
      bool canGoUp = up >= 0; bool canGoDown = down < Game.Size;

      if (canGoLeft)
      {
        Neighbours.Add(new(Row, left));
        // NumOfNeighbours++;

        if (canGoDown) Neighbours.Add(new(down, left));

        if (canGoUp) Neighbours.Add(new(up, left));
      }

      if (canGoRight)
      {
        Neighbours.Add(new(Row, right));

        if (canGoUp) Neighbours.Add(new(up, right));
        if (canGoDown) Neighbours.Add(new(down, right));
      }

      if (canGoUp) Neighbours.Add(new(up, Col));

      if (canGoDown) Neighbours.Add(new(down, Col));
    }
  }

}
