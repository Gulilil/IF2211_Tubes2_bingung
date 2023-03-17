using System;
using System.IO;

namespace src
{
    public class Map
    {
        private int row;
        private int col;
        private int nTreasure;
        // private char[] buffer;

        public Map() {
            row = 0;
            col = 0;
            nTreasure = 0;
        }

        public Map(int r, int c, int n){
            row = r;
            col = c;
            nTreasure = n;
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

        public int getRow(){
            return row;
        }        

        public int getCol(){
            return col;
        }

        // public int[] IdentifyFile(string textFile)
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