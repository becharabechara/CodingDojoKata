using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace GameOfLife
{
    public class Cell
    {
        public Cell(int pX,int pY,int vV)
        {
            this.PositionX = pX;
            this.PositionY = pY;
            this.Value = vV;
        }
        public Cell(int pX, int pY)
        {
            this.PositionX = pX;
            this.PositionY = pY;
        }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Value { get; set; }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            else
            {
                Cell c = (Cell) obj;
                return (PositionX == c.PositionX && PositionY == c.PositionY);
            }
        }
        public override int GetHashCode()
        {
            return (PositionX << 2) ^ PositionY;
        }
    }
}
