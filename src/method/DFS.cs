using System;

namespace src
{
    public class DFS : Solver
    {
        private bool TSP;

        // ctor
        public DFS(){
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
        public void solve(Map m) {
            if (this.TSP){

            } else {

            }
        }
    }
}