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

        public Point(Point p)
        {
            this.row = p.getRow();
            this.col = p.getCol();
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

        public void copyPoint(Point p){
            this.row = p.getRow();
            this.col = p.getCol();
        }
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
        public bool isTheSame(Point p){
            return (this.getRow() == p.getRow() && this.getCol() == p.getCol());
        }

        public bool isLeftOf(Point p)
        {
            return (this.getRow() == p.getRow() && this.getCol() == p.getCol()-1);
        }
        public bool isRightOf(Point p)
        {
            return (this.getRow() == p.getRow() && this.getCol() == p.getCol()+1);
        }

        public bool isUpOf(Point p)
        {
            return (this.getRow() == p.getRow()-1 && this.getCol() == p.getCol());
        }
        public bool isDownOf(Point p)
        {
            return (this.getRow() == p.getRow()+1 && this.getCol() == p.getCol());
        }

        // print and display
        public void displayPoint(){
            Console.Write("("+this.row +", "+this.col+")");
        }

    }
}