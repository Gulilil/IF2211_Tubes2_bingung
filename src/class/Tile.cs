using System;

namespace src
{
    public class Tile 
    {
        private char value;
        private int visitedCount;

        // ctor
        public Tile() {
            value = '.';
            visitedCount = 0;
        }

        // setter getter
        public void setValue(char c) {
            value = c;
        }
        public void setVisitedCount(int vc){
            visitedCount = vc;
        }
        public char getValue(){
            return value;
        }
        public int getVisitedCount(){
            return visitedCount;
        }

        // other method
        public void increaseVisitedCount(){
            visitedCount++;
        }

    }
}