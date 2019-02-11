using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace GameOfLife
{
    public class Cell
    {
        public Cell(int pX, int pY, int vV)
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

        public int PositionX { get; }
        public int PositionY { get; }
        public int Value { get; }

        protected bool Equals(Cell other)
        {
            return PositionX == other.PositionX && PositionY == other.PositionY && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PositionX;
                hashCode = (hashCode * 397) ^ PositionY;
                hashCode = (hashCode * 397) ^ Value;
                return hashCode;
            }
        }
    }
}
