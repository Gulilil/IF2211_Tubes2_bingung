using System;
using System.IO;

namespace src
{
    public class Map
    {
        private int row;
        private int col;
        private int nTreasure;
        private Point curLoc;
        // private char[] buffer;

        public Map() {
            row = 0;
            col = 0;
            nTreasure = 0;
            curLoc = new Point();
        }

        public Map(int r, int c, int n, int curR, int curL){
            row = r;
            col = c;
            nTreasure = n;
            curLoc = new Point(curR, curL);
            // for (int i = 0; i < row ; i++){
            //     for(int j = 0; j < col; j++){
            //         buffer[i][j] = m[i][j];
            //     }
            // }
        }

        public void setRow(int r){
            row = r;
        }

        public void setCol(int c){
            col = c;
        }

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

        public int getRow(){
            return row;
        }        

        public int getCol(){
            return col;
        }

        public Point getCurLoc(){
            return this.curLoc;
        }


        // public void IdentifyFile(string textFile)
        // {
        //     int[2] data = {0,0};
        //     string[] lines = File.ReadAllLines(textFile);
        //     foreach (string line in lines);
        //     Console.WriteLine(lines);

        //     return data;
        // }

        // public char[,,] ReadFile()
        // {
        //     Console.WriteLine("Enter File Name: ");
        //     string fileName = Console.ReadLine();
        //     string textFile = string.Concat("../test/", fileName);
        //     int[2] data = IdentifyFile(textFile);

        //     char[,,] charMatrix = { {'o','o'}, {'o','o'}};
        //     return charMatrix;
            
        // }
    }
}