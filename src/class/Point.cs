using System;

namespace src
{
    public class Point
    {
        private int row;
        private int col;

        // ctor
        public Point ()
        {
            row = 0; col = 0;
        }

        public Point(int r, int c)
        {
            row = r; col = c;
        }

        // setter getter
        public int getRow()
        {
            return row;
        }

        public int getCol()
        {
            return col;
        }

        public void setRow(int r)
        {
            this.row = r;
        }

        public void setCol(int c)
        {
            this.col = c;
        }

        // other methods
        public void goLeft()
        {
            this.col++;
        }

        public void goRight()
        {
            this.col--;
        }

        public void goUp() 
        {
            this.row--;
        }
        
        public void goDown()
        {
            this.row++;
        }
    }
}