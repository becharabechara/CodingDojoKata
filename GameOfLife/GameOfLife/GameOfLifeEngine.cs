using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfLife
{
    public class GameOfLifeEngine
    {
        private static List<Cell> CellsList = new List<Cell>();
        private static int GridM = 0;
        private static int GridN = 0;
        public static void Create(int m, int n, string input)
        {
            CellsList.Clear();
            GridM = m;
            GridN = n;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    CellsList.Add(new Cell(i, j, Int32.Parse(input[i * n + j].ToString())));
                }
            }
        }

        public static string Generate()
        {
            var ret = string.Empty;
            foreach (var vCell in CellsList)
            {
                if (vCell.Value == 1)
                    ret += KillCell(vCell);
                else
                    ret += vCell.Value;
            }
            return ret;
        }
        public static Cell GetCell(int posX, int posY)
        {
            return CellsList.Find(x => x.Equals(new Cell(posX, posY)));
        }
        public static List<Cell> GetAdjacents(Cell c)
        {
            var result = new List<Cell>();
            var pX = CellsList.Find(x => x.Equals(c)).PositionX;
            var pY = CellsList.Find(x => x.Equals(c)).PositionY;
            for (int j = pX - 1; j <= pX + 1; j++)
            for (int i = pY - 1; i <= pY + 1; i++)
                if (i >= 0 && j >= 0 && i < GridN && j < GridM && !(j == pX && i == pY))
                    result.Add(GetCell(j, i));
            return result;
        }

        private static int KillCell(Cell c)
        {
            var count = 0;
            var res = GetAdjacents(c);
            foreach (var v in res)
                if (v.Value == 1)
                    count++;
            if (count < 2 || count > 3) return 0;
            else return c.Value;
        }
    }
}