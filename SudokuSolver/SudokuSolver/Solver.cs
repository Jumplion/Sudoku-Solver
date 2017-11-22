using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Solver
    {
        public static int[,] map = new int[9, 9];

        static void Main(string[] args)
        {
            ReadFile("SudokuGrid.txt");
            PrintMap();

            // Find first empty spot
            Coordinate start = FindNextEmpty();

            if (start.IsNull())
                return;

            Console.WriteLine();

            for(int n = 1; n <= 9; n++)
            {
                if (Check(n, start))
                    PrintMap();
            }

            Console.Read();
        }

        static bool Check(int num, Coordinate c)
        {
            // There are no more empty spaces, so we must have found a solution
            if(c.IsNull())
                return true;

            if(CheckCell(num, c) && CheckColumn(num, c.x) && CheckRow(num, c.y))
            {
                map[c.x, c.y] = num;

                Coordinate next = FindNextEmpty();

                for(int n = 1; n <= 9; n++)
                {
                    if (Check(n, next))
                        return true;
                }

                map[c.x, c.y] = 0;

                return false;
            }

            return false;
        }

        static bool CheckCell(int num, Coordinate c)
        {
            int cellX = c.x;
            int cellY = c.y;

            if (cellY < 3)
            {
                cellY = 0;
                if (cellX < 3)
                    cellX = 0;
                else if (cellX < 6)
                    cellX = 3;
                else
                    cellX = 6;
            }

            else if (c.y < 6)
            {
                cellY = 3;
                if (cellX < 3)
                    cellX = 0;
                else if (cellX < 6)
                    cellX = 3;
                else
                    cellX = 6;
            }

            else
            {
                cellY = 6;
                if (cellX < 3)
                    cellX = 0;
                else if (cellX < 6)
                    cellX = 3;
                else
                    cellX = 6;
            }

            if (map[cellX, cellY] == num     || map[cellX + 1, cellY] == num     || map[cellX + 2, cellY] == num
             || map[cellX, cellY + 1] == num || map[cellX + 1, cellY + 1] == num || map[cellX + 2, cellY + 1] == num
             || map[cellX, cellY + 2] == num || map[cellX + 1, cellY + 2] == num || map[cellX + 2, cellY + 2] == num
                )
                return false;

            return true;
        }

        static bool CheckColumn(int num, int x)
        {
            for(int y = 0; y < 9; y++)
            {
                if(map[x,y] == num)
                {
                    return false;
                }
            }

            return true;
        }

        static bool CheckRow(int num, int y)
        {
            for (int x = 0; x < 9; x++)
            {
                if (map[x, y] == num)
                {
                    return false;
                }
            }

            return true;
        }

        static Coordinate FindNextEmpty()
        {
            for (int Y = 0; Y < 9; Y++)
            {
                for (int X = 0; X < 9; X++)
                {
                    if (map[X, Y] == 0)
                        return new Coordinate(X, Y);
                }
            }

            return new Coordinate(-1, -1);
        }

        static void ReadFile(string file)
        {
            string[] lines = File.ReadAllLines(@"F:\Documents\Repositories\Sudoku Solver\SudokuSolver\SudokuSolver\" + file);

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] != ' ')
                        map[x, y] = (int)Char.GetNumericValue(lines[y][x]);
                    else
                        map[x, y] = 0;
                }
            }
        }

        static void PrintMap()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (map[x, y] == 0)
                        Console.Write("- ");
                    else
                        Console.Write(map[x, y] + " ");
                }
                Console.Write("\n");
            }
            return;
        }
    }

    public struct Coordinate
    {
        public int x, y;

        public Coordinate(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public bool IsNull()
        {
            return (x < 0 || y < 0);
        }
    }
}