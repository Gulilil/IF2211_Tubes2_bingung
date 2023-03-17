using System;

namespace src
{
    public class Map
    {
        private int row;
        private int col;
        private int nTreasure;
        private Point curLoc;
        private char[,] buffer;

        // ctor
        public Map() {
            this.row = 0;
            this.col = 0;
            this.nTreasure = 0;
            this.curLoc = new Point();
            this.buffer = new char[0,0] {};
        }

        public Map(int r, int c, int n, Point cl){
            this.row = r;
            this.col = c;
            this.nTreasure = n;
            this.curLoc = cl;
            this.buffer = new char[0,0] {};
        }

        // setter getter
        public void setRow(int r){
            row = r;
        }

        public void setCol(int c){
            col = c;
        }

        public int getRow(){
            return row;
        }        

        public int getCol(){
            return col;
        }

        public Point getCurLoc(){
            return this.curLoc;
        }
        public int getnTreasure() {
            return this.nTreasure;
        }

        // other methods
        public void changeCurLoc(char c){
            if (c == 'L'){
                this.curLoc.goLeft();
            } else if (c == 'R'){
                this.curLoc.goRight();
            } else if (c == 'U'){
                this.curLoc.goUp();
            } else if (c == 'D'){
                this.curLoc.goDown();
            }
        }

        public void displayMap(){
            for(int i = 0; i < this.row; i++){
                Console.Write("[ ");
                for (int j = 0; j < this.col; j++){
                    if (j == this.col-1){
                        Console.Write(this.buffer[i,j]);
                    } else {
                        Console.Write(this.buffer[i,j]);
                        Console.Write(' ');
                    }
                }
                Console.Write(" ]");
                Console.WriteLine();
            }
        }

        public void IdentifyFile(string textFile)
        {
            int nCol = 0;
            int nRow = 0;
            string[] lines = File.ReadAllLines(textFile);
            foreach (string line in lines){
                if (nCol == 0){
                    foreach(char c in line){
                        if (c != ' '){
                            nCol++;
                        }
                    }
                }
                nRow++;
            }
            this.setCol(nCol);
            this.setRow(nRow);
        }

        public void ReadFile()
        {
            Console.Write("Enter File Name: ");
            string? fileName = Console.ReadLine();
            fileName = fileName + ".txt";
            string path = Directory.GetCurrentDirectory();
            string fullPath = path + "/test/" +fileName;

            IdentifyFile(fullPath);
            this.buffer = new char[this.row, this.col];
            string[] lines = File.ReadAllLines(fullPath);

            int nCol = 0;
            int nRow = 0;
            foreach(string line in lines){
                nRow = 0;
                foreach(char c in line){
                    if(c != ' '){
                        this.buffer[nCol, nRow] = c;

                        if (c == 'T'){
                            this.nTreasure++;
                        } else if ( c == 'K'){
                            this.curLoc = new Point(nRow, nCol);
                        }
                        nRow++;
                    }
                }
                nCol++;
            }
            
            
        }
    }
}