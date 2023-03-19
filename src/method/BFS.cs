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
        public bool isAllTreasureTaken(List<Point> path, Point[] tLoc){
            for (int i = 0; i < tLoc.Length; i++){
                bool found = path.Contains(tLoc[i]);
                if (!found){
                    return false;
                }
            }
            return true;
        }

        public List<Point> solve(Route r, Map m){
            Queue<Point> q = new Queue<Point>(); 
            q.Enqueue(m.getStartLoc());
            while (q.Count > 0){
                m.setCurLoc(q.Dequeue());
                Point cl = m.getCurLoc();
                visits[cl] = 1;
                if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
                {
                    // check Up
                    
                }
                if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()-1) != 'X')
                {
                    // check Left
                    
                }
                if (cl.getCol() != m.getCol() - 1 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) != 'X')
                {
                    // check Right
                    
                }
                if (cl.getRow() != m.getRow() - 1 && m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) != 'X')
                {
                    // check Down
    
                }
            
            }
        }
    }
}