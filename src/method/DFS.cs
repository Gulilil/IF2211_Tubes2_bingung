using System;

namespace src
{
    public class DFS : Solver
    {
        private bool TSP;
        private Stack<Point> availPath;
        private Stack<Point> takenPath;
        private Stack<Point> recordedPath;

        // ctor
        public DFS(){
            this.TSP = false;
            this.availPath = new Stack<Point> ();
            this.takenPath = new Stack<Point> ();
            this.recordedPath = new Stack<Point>();
        }

        // setter getter
        public bool getTSP(){
            return this.TSP;
        }

        public void setTSP(bool tsp){
            this.TSP = tsp;
        }

        public Stack<Point> getAvailPath(){
            return this.availPath;
        }
        public Stack<Point> getTakenPath()
        {
            return this.takenPath;
        }
        public Stack<Point> getRecordedPath()
        {
            return this.recordedPath;
        }

        // other methods

        public bool isPathAlreadyTaken(Stack<Point> paths, Point p){
            bool found = false;
            Stack<Point> temp = new Stack<Point>();
            while(paths.Count > 0){
                // moving elements to temp while checking
                Point top = paths.Pop();
                if (top.isTheSame(p)){
                    found = true;
                }
                temp.Push(top);
            }
            while(temp.Count > 0)
            {
                // moving elements back to paths
                Point top = temp.Pop();
                paths.Push(top);
            }
            return found;
        }

        public Point[] searchAdjacentPath(Map m){
            Point[] res = new Point[] {};
            Point cl = m.getCurLoc();
            if (cl.getRow() != m.getRow() - 1 && m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) != 'X')
            {
                // check Down
                Point temp = new Point(cl.getRow() + 1, cl.getCol());
                res = this.insertLastPaths(res, temp);
            }
            if (cl.getCol() != m.getCol() - 1 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) != 'X')
            {
                // check Right
                Point temp = new Point(cl.getRow(), cl.getCol() + 1);
                res = this.insertLastPaths(res, temp);
            }
            if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
            {
                // check Up
                Point temp = new Point(cl.getRow() - 1, cl.getCol());
                res = this.insertLastPaths(res, temp);
            }
            if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()-1) != 'X'){
                // check Left
                Point temp = new Point(cl.getRow(), cl.getCol()-1);
                res = this.insertLastPaths(res, temp);
            } 
            return res;
        }

        public void solve(Map m) {
            if (this.TSP){

            } else {
                this.startTime();
                int treasureGet = 0;
                while (treasureGet < m.getnTreasure()){
                    // if current location coordinate is the treasure
                    if (m.getValueAtCoordinate(m.getCurLoc()) == 'T'){
                        treasureGet++;
                    }

                    // searching algorithm
                    Point[] availableCoordinates = searchAdjacentPath(m);
                    int insertedPath = 0;
                    for (int i = 0; i < availableCoordinates.Length; i++){
                        // check if the available coordinate is already taken in the takenPath
                        if (!isPathAlreadyTaken(this.recordedPath, availableCoordinates[i])){
                            this.availPath.Push(availableCoordinates[i]);
                            this.setNodes(this.getNodes()+1);
                            insertedPath++;
                        }
                    }
                    
                    if (insertedPath == 0)
                    {
                        // backtracking
                        Point top = this.takenPath.Pop();
                        this.recordedPath.Push(m.getCurLoc());
                        m.setCurLoc(top);
                    }
                    else
                    {
                        // changing curLoc to the first element of the availPath
                        Point top = this.availPath.Pop();
                        this.takenPath.Push(m.getCurLoc());
                        this.recordedPath.Push(m.getCurLoc());
                        m.setCurLoc(top);
                    }
                }
                this.setSteps(this.recordedPath.Count);
                copySolutionPathsDFS(recordedPath);
                convertPathsToRoutes();
                this.stopTime();
            }
        }

        // print and display
        public void displayPath(Stack<Point> paths)
        {
            Stack<Point> temp = new Stack<Point>();
            while (paths.Count > 0)
            {
                Point top = paths.Pop();
                top.displayPoint();
                temp.Push(top);
            }
            while (temp.Count > 0)
            {
                Point top = temp.Pop();
                paths.Push(top);
            }
        }
    }
}