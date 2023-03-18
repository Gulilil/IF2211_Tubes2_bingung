using System;

namespace src
{
    public class DFS : Solver
    {
        private bool TSP;
        private Stack<Point> curPath;

        // ctor
        public DFS(){
            this.TSP = false;
            this.curPath = new Stack<Point> ();
        }

        // setter getter
        public bool getTSP(){
            return this.TSP;
        }

        public void setTSP(bool tsp){
            this.TSP = tsp;
        }

        // other methods
        public void solve(Map m) {
            if (this.TSP){

            } else {
                int treasureGet = 0;
                while (treasureGet < m.getnTreasure()){
                    // if current location coordinate is the treasure
                    if (m.getValueAtCoordinate(m.getCurLoc()) == 'T'){
                        treasureGet++;
                    }
                    
                }
            }
        }
    }
}