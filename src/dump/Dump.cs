/* Kumpulan Algoritma sayang dibuang


DFS

        private Stack<Point> availPath;
        private Stack<Point> takenPath;
        private Stack<Point> recordedPath;

        this.availPath = new Stack<Point> ();
        this.takenPath = new Stack<Point> ();
        this.recordedPath = new Stack<Point>();

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
            if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()-1) != 'X'){
                // check Left
                Point temp = new Point(cl.getRow(), cl.getCol()-1);
                res = this.insertLastPaths(res, temp);
            } 
            if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
            {
                // check Up
                Point temp = new Point(cl.getRow() - 1, cl.getCol());
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






*/