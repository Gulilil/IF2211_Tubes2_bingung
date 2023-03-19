using System;

namespace src
{
    public class BFS : Solver
    {
        private bool TSP;

        // ctor
        public BFS(){
            this.TSP = false;
        }

        // setter getter
        public bool getTSP(){
            return this.TSP;
        }

        public void setTSP(bool tsp){
            this.TSP = tsp;
        }

        // other methods
        public bool isAllTreasureTaken(Point[] tLoc, List<Point> path){
            for (int i = 0; i < tLoc.Length; i++){
                bool found = path.Contains(tLoc[i]);
                if (!found){
                    return false;
                }
            }
            return true;
        }
        public void solve(Map m) {
            
        }
    }
}