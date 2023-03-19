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

        public bool isPathAlreadyTaken(Stack<Point> paths, Point p){
            return paths.Contains(p);
        }

        // public Point[] sortPriorityCoordinate(Point[] p){
        //     // using insertion sort
        //     if (p.Length > 1){
        //         for (int i = 1; i < p.Length-1; i++){
        //             int j = i-1;
        //             Point temp = p[j];
        //             while(temp.getVisitedCount() < p[j].getVisitedCount() && j > 0){
        //                 p[j+1].copyPoint(p[j]);
        //                 j = j-1;
        //             }
        //             if (temp.getVisitedCount() >= p[j].getVisitedCount()){
        //                 p[j+1].copyPoint(temp);
        //             } else {
        //                 p[j+1].copyPoint(p[j]);
        //                 p[j].copyPoint(temp);
        //             }
        //         }
        //     }
        //     return p;
        // }

        public Point[] getPriorityCoordinates(Map m){
            Point[] coordinates = new Point[] {};
            Point cl = m.getCurLoc();

            if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
            {
                // check Up
                Point newCl = new Point(cl);
                newCl.goUp();  
                coordinates = insertLastPaths(coordinates, newCl);     
            }
            if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()-1) != 'X')
            {
                // check Left
                Point newCl = new Point(cl);
                newCl.goLeft();
                coordinates = insertLastPaths(coordinates, newCl);   
            }
            if (cl.getCol() != m.getCol() - 1 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) != 'X')
            {
                // check Right
                Point newCl = new Point(cl);
                newCl.goRight();
                coordinates = insertLastPaths(coordinates, newCl);   
            }
            if (cl.getRow() != m.getRow() - 1 && m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) != 'X')
            {
                // check Down
                Point newCl = new Point(cl);
                newCl.goDown();
                coordinates = insertLastPaths(coordinates, newCl);   
            }
            // coordinates = sortPriorityCoordinate(coordinates);

            return coordinates;
        }

        
        public Stack<Point> solve(Map m, Stack<Point> p){
            Point cl = m.getCurLoc();
            p.Push(cl);
            nodes++;
            cl.displayPoint(); Console.WriteLine();
            displayPath(p);

            if (isAllTreasureTaken(p, m.getTreasureLocations())){
                // System.Console.WriteLine("hehe");
                return p;
            }

            int iteration = 0;
            Point[] availableCoordinates = getPriorityCoordinates(m);
            while(iteration < availableCoordinates.Length){
                m.setCurLoc(availableCoordinates[iteration]);
                if (isAllTreasureTaken(solve(m, p), m.getTreasureLocations())){
                    return p;
                }
                iteration++;
            }

            // backtrack
            // Console.WriteLine("Mentok");
            Point dump = p.Pop();
            return new Stack<Point> ();
        }
        

        public bool isAllTreasureTaken(Stack<Point> p, Point[] tLoc){
            for(int i = 0; i < tLoc.Length; i++){
                if(!p.Contains(tLoc[i])){
                    return false;
                }
            }
            return true;
        }

        public void getSolution(Route r, Map m, Stack<Point> p){
            startTime();
            Stack<Point> solution = solve(m, p);

            this.steps = solution.Count;
            copySolutionPathsDFS(solution);
            convertPathsToRoutes();
            stopTime();
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

        public Stack<Point> solveTest(Route r, Map m, Stack<Point> p){
            Point cl = m.getCurLoc();
            r.setParent(cl);
            p.Push(r.getParent());
            nodes++;
            // cl.displayPoint(); Console.WriteLine();
            // displayPath(p);

            if (isAllTreasureTaken(p, m.getTreasureLocations())){
                // System.Console.WriteLine("hehe");
                return p;
            }

            if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
            {
                // check Up
                Point newCl = new Point(cl);
                newCl.goUp();
                if (!isPathAlreadyTaken(p, newCl)){
                    m.setCurLoc(newCl);
                    r.setUpChild(newCl);
                    // Console.WriteLine("Atas");
                    if (isAllTreasureTaken(solveTest(r.getUpChild(), m, p), m.getTreasureLocations())){
                        return p;
                    }
                }
            }
            if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()-1) != 'X')
            {
                // check Left
                Point newCl = new Point(cl);
                newCl.goLeft();
                if (!isPathAlreadyTaken(p, newCl)){
                    m.setCurLoc(newCl);
                    r.setLeftChild(newCl);
                    // Console.WriteLine("Kiri");
                    if (isAllTreasureTaken(solveTest(r.getLeftChild(), m, p), m.getTreasureLocations())){
                        return p;
                    }
                }
            }
            if (cl.getCol() != m.getCol() - 1 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) != 'X')
            {
                // check Right
                Point newCl = new Point(cl);
                newCl.goRight();
                if (!isPathAlreadyTaken(p, newCl)){
                    m.setCurLoc(newCl);
                    r.setRightChild(newCl);
                    // Console.WriteLine("Kanan");
                    if (isAllTreasureTaken(solveTest(r.getRightChild(), m, p), m.getTreasureLocations())){
                        return p;
                    }
                }
            }
            if (cl.getRow() != m.getRow() - 1 && m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) != 'X')
            {
                // check Down
                Point newCl = new Point(cl);
                newCl.goDown();
                if (!isPathAlreadyTaken(p, newCl)){
                    m.setCurLoc(newCl);
                    r.setDownChild(newCl);
                    // Console.WriteLine("Bawah");
                    if (isAllTreasureTaken(solveTest(r.getDownChild(), m, p), m.getTreasureLocations())){
                        return p;
                    }
                }
            }
            // backtrack
            // Console.WriteLine("Mentok");
            Point dump = p.Pop();
            return new Stack<Point> ();
        }


    }

}