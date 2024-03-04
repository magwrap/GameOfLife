using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife
{
  public class Grid
  {
    public Cell[,] cells;
    private readonly Random rnd = new();
    private delegate void CellsAction(int i, int j);
    private delegate void OptionalAction();
    public Grid()
    {
      cells = new Cell[Game.Size, Game.Size];
      GenerateGrid();
    }
    //TODO: change matrix to some other better structure

    private static void LoopThroughCells(
      CellsAction action, OptionalAction? optionalAction = null
    )
    {
      int i, j;
      for (i = 0; i < Game.Size; i++)
      {
        for (j = 0; j < Game.Size; j++)
        {
          action(i, j);
        }
        optionalAction?.Invoke();
      }

    }
    private void GenerateGrid() // Generates matrix of given size filled with cells
    {
      LoopThroughCells(delegate (int i, int j)
      {
        cells[i, j] = new Cell(i, j, rnd.NextDouble() > 0.5);
      });
    }

    public void DisplayGrid()
    {
      Cell cell;
      bool willItLive;
      LoopThroughCells(
        delegate (int i, int j)
        {
          cell = cells[i, j];

          willItLive = WillCellLive(cell);

          // if (!willItLive) Console.ForegroundColor = ConsoleColor.DarkRed;

          Console.Write($"{cell} ");

          cell.IsAlive = willItLive;
          // Console.ForegroundColor = ConsoleColor.White;
        },
        Console.WriteLine
      );
    }

    public void NextGeneration()
    {
      LoopThroughCells(
        delegate (int i, int j)
        {
          cells[i, j].IsAlive = WillCellLive(cells[i, j]);
        }
      );
    }

    private bool WillCellLive(Cell cell)
    {
      int numOfNeighbours;
      numOfNeighbours = 0;

      foreach (var (row, col) in cell.Neighbours)
      {
        if (cells[row, col].IsAlive)
          numOfNeighbours++;
      }

      return DecideCellsFate(numOfNeighbours, cell.IsAlive);

    }
    private static bool DecideCellsFate(int numOfNeighbours, bool isAlive)
    {
      if (isAlive)
      {
        return numOfNeighbours switch
        {
          2 or 3 => true,
          _ => false,
        };
      }

      else if (numOfNeighbours == 3) return true;

      return false;
    }
  }
}
