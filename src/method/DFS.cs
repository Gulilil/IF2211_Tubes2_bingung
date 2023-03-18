using System;

namespace src
{
    public class DFS : Solver
    {
        private bool TSP;
        private Stack<Point> curPath;
        private Stack<Point> takenPath;

        // ctor
        public DFS(){
            this.TSP = false;
            this.curPath = new Stack<Point> ();
            this.takenPath = new Stack<Point> ();
        }

        // setter getter
        public bool getTSP(){
            return this.TSP;
        }

        public void setTSP(bool tsp){
            this.TSP = tsp;
        }

        public Stack<Point> getCurPath(){
            return this.curPath;
        }

        // other methods

        public bool isPathAlreadyTaken(Stack<Point> paths, Point p){
            bool found = false;
            int count = 0;
            Point[] temp = new Point[paths.Count];
            while(paths.Count > 0){
                Point top = paths.Pop();
                if (top.isTheSame(p)){
                    found = true;
                }
                temp[count] = top;
                count++;
            }
            for (int i = temp.Length-1; i >=0 ; i++){
                paths.Push(temp[i]);
            }
            return found;
        }

        public Point[] searchAdjacentPath(Map m){
            Point[] res = new Point[] {};
            Point cl = m.getCurLoc();
            if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()-1) != 'X'){
                // check Left
                Point temp = new Point(cl.getRow(), cl.getCol()-1);
                res = this.insertLastPaths(res, temp);
            } 
            if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow()-1, cl.getCol()) != 'X'){
                // check Up
                Point temp = new Point(cl.getRow()-1, cl.getCol());
                res = this.insertLastPaths(res, temp);
            }
            if (cl.getCol() != m.getCol()-1 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()+1) != 'X'){
                // check Right
                Point temp = new Point(cl.getRow(), cl.getCol()+1);
                res = this.insertLastPaths(res, temp);
            }
            if (cl.getRow() != m.getRow() -1 && m.getValueAtCoordinate(cl.getRow()+1, cl.getCol()) != 'X'){
                // check Down
                Point temp = new Point(cl.getRow()+1, cl.getCol());
                res = this.insertLastPaths(res, temp);
            }
            return res;
        }

        public void solve(Map m) {
            if (this.TSP){

            } else {
                int treasureGet = 0;
                while (treasureGet < m.getnTreasure()){
                    // if current location coordinate is the treasure
                    if (m.getValueAtCoordinate(m.getCurLoc()) == 'T'){
                        treasureGet++;
                    }

                    // searching algorithm
                    Point[] availablePath = searchAdjacentPath(m);
                    for (int i = 0; i < availablePath.Length; i++){
                        if (!isPathAlreadyTaken(this.curPath, availablePath[i])){
                            this.curPath.Push(availablePath[i]);
                        }
                    }

                    treasureGet = m.getnTreasure();
                    
                }
            }
        }

        public void displayPath(Stack<Point> paths){
            while (paths.Count > 0){
                Point top = paths.Pop();
                top.displayPoint();
            }
        }
    }
}