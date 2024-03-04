using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Game
    {
        private static int size = 20;
        public static int Size { get => size; set => size = value; }
        private readonly Grid? grid = new();
        public void Play()
        {
            System.ConsoleKey[] consoleBreakKeys = { ConsoleKey.Enter, ConsoleKey.Spacebar };
            while (true)
            {
                if (Console.KeyAvailable && consoleBreakKeys.Contains(Console.ReadKey(true).Key))
                {
                    break;
                }

                Console.Clear();
                grid?.DisplayGrid();
                Console.WriteLine("Press enter or space to exit");
                grid?.NextGeneration();
                Thread.Sleep(1000);
            }
        }
    }
}
