using System;
using System.Diagnostics;
using System.IO;

namespace Class
{
    public class Map
    {
        private int row;
        private int col;
        private int nTreasure;
        private Point[] treasureLocs;
        private Point startLoc;
        private Point curLoc;
        private Tile[,] buffer;
        private bool valid;

        // ctor
        public Map() {
            this.row = 0;
            this.col = 0;
            this.nTreasure = 0;
            this.treasureLocs = new Point[] {};
            this.startLoc = new Point();
            this.curLoc = new Point();
            this.buffer = new Tile[0,0] {};
            this.valid = true;
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

        public void setCurLoc(Point p)
        {
            this.curLoc = p;
        }

        public Point getCurLoc(){
            return this.curLoc;
        }
        public Point getStartLoc()
        {
            return this.startLoc;
        }
        public int getnTreasure() {
            return this.nTreasure;
        }
        public char getValueAtCoordinate(int r, int c){
            return this.buffer[r,c].getValue();
        }
        public char getValueAtCoordinate(Point p){
            return this.buffer[p.getRow(), p.getCol()].getValue();
        }
        public void setValueAtCoordinate(Point p, char c){
            this.buffer[p.getRow(), p.getCol()].setValue(c);
        }
        public void setVCAtCoordinate(Point p, int n){
            this.buffer[p.getRow(), p.getCol()].setVisitedCount(n);
        }
        public int getVCAtCoordinate(int r, int c)
        {
            return this.buffer[r, c].getVisitedCount();
        }
        public int getVCAtCoordinate(Point p){
            return this.buffer[p.getRow(), p.getCol()].getVisitedCount();
        }
        public void increaseVCAtCoordinate(Point p){
            this.buffer[p.getRow(), p.getCol()].increaseVisitedCount();
        }
        public Point[] getTreasureLocations(){
            return this.treasureLocs;
        }
        public void setValidTrue()
        {
            this.valid = true;
        }
        public void setValidFalse()
        {
            this.valid = false;
        }
        public bool getValid()
        {
            return this.valid;
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

        public void addTreasureLocation(int r, int c){
            Point treasure = new Point(r,c);
            Point[] temp = (Point[])this.treasureLocs.Clone();
            this.treasureLocs = new Point[temp.Length+1];
            for (int i = 0; i < this.treasureLocs.Length; i++){
                if (i == this.treasureLocs.Length -1){
                    this.treasureLocs[i] = treasure;
                } else {
                    this.treasureLocs[i] = temp[i];
                }
            }
        }

        public void getInfo(){
            if (getValid())
            {
                System.Console.WriteLine("==========================");
                displayMap();
                Console.WriteLine("Row: " + getRow());
                Console.WriteLine("Col: " + getCol());
                Console.WriteLine("Treasure Amount: " + getnTreasure());
                Console.Write("Treasure Locations: ");
                displayTreasureLocations();
                Console.Write("Starting Location: ");
                this.startLoc.displayPoint();
                System.Console.WriteLine();
            }
        }

        public void resetMap()
        {
            if (getValid())
            {
                this.curLoc.copyPoint(this.startLoc);

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        setVCAtCoordinate(new Point(i, j), 0);
                    }
                }
            }
        }

        // print and display
        public void displayMap(){
            for(int i = 0; i < this.row; i++){
                Console.Write("[ ");
                for (int j = 0; j < this.col; j++){
                    if (j == this.col-1){
                        Console.Write(this.buffer[i,j].getValue());
                    } else {
                        Console.Write(this.buffer[i,j].getValue());
                        Console.Write(' ');
                    }
                }
                Console.Write(" ]");
                Console.WriteLine();
            }
        }

        public void displayTreasureLocations(){
            Console.Write("(");
            for (int i = 0; i < this.treasureLocs.Length ; i++){
                if (i == this.treasureLocs.Length - 1){
                    this.treasureLocs[i].displayPoint();
                } else {
                    this.treasureLocs[i].displayPoint();
                    Console.Write(", ");
                }
            }
            Console.WriteLine(")");
        }

        // read file
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
                            if (c != 'K' && c != 'X' && c!= 'R' && c != 'T')
                            {
                                setValidFalse();
                            }
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
            string fileName = Console.ReadLine();
            fileName = fileName + ".txt";
            string path = Directory.GetCurrentDirectory();
            string fullPath = path + "/test/" +fileName;

            IdentifyFile(fullPath);
            if (getValid())
            {
                this.buffer = new Tile[this.row, this.col];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        this.buffer[i, j] = new Tile();
                    }
                }
                string[] lines = File.ReadAllLines(fullPath);

                int nCol = 0;
                int nRow = 0;
                foreach (string line in lines)
                {
                    nCol = 0;
                    foreach (char c in line)
                    {
                        if (c != ' ')
                        {
                            this.buffer[nRow, nCol].setValue(c);

                            if (c == 'T')
                            {
                                this.nTreasure++;
                                addTreasureLocation(nRow, nCol);
                            }
                            else if (c == 'K')
                            {
                                this.curLoc = new Point(nRow, nCol);
                            }
                            nCol++;
                        }
                    }
                    nRow++;
                }
                this.startLoc.copyPoint(this.curLoc);
            } 
            else
            {
                Console.WriteLine("Map Reading Failed. Invalid Map Detected.");
            }
        }

        public void ReadFileAtPath(String path)
        {
            IdentifyFile(path);
            if (getValid())
            {
                this.buffer = new Tile[this.row, this.col];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        this.buffer[i, j] = new Tile();
                    }
                }
                string[] lines = File.ReadAllLines(path);

                int nCol = 0;
                int nRow = 0;
                foreach (string line in lines)
                {
                    nCol = 0;
                    foreach (char c in line)
                    {
                        if (c != ' ')
                        {
                            this.buffer[nRow, nCol].setValue(c);

                            if (c == 'T')
                            {
                                this.nTreasure++;
                                addTreasureLocation(nRow, nCol);
                            }
                            else if (c == 'K')
                            {
                                this.curLoc = new Point(nRow, nCol);
                            }
                            nCol++;
                        }
                    }
                    nRow++;
                }
                this.startLoc.copyPoint(this.curLoc);
          
                Debug.WriteLine("Masukkan benar");
            } 
            else
            {
                
                Debug.WriteLine("Masukkan salah");
            }
        }
    }
}