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

        public void getInfo(){
            System.Console.WriteLine("==========================");
            Console.WriteLine("Execution Time: " + getExecutionTime() + " ms");
            Console.WriteLine("Total Nodes: "+ getNodes());
            Console.WriteLine("Total Steps: "+ getSteps());
            Console.Write("Solution Route: ");
            displaySolutionRoutes();
            Console.WriteLine("Solution Paths: ");
            displaySolutionPaths();
        }

        public bool isPathAlreadyTaken(Stack<Point> paths, Point p){
            // bool found = false;
            // Stack<Point> temp = new Stack<Point>();
            // while(paths.Count > 0){
            //     // moving elements to temp while checking
            //     Point top = paths.Pop();
            //     if (top.isTheSame(p)){
            //         found = true;
            //     }
            //     temp.Push(top);
            // }
            // while(temp.Count > 0)
            // {
            //     // moving elements back to paths
            //     Point top = temp.Pop();
            //     paths.Push(top);
            // }
            return paths.Contains(p);
        }

        // public Point[] getPriorityCoordinate(Map m, Stack<Point> p){
        //     Point[] coordinates = new Point[] {};

        //     if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
        //     {
        //         // check Up
        //         Point newCl = new Point(cl);
        //         newCl.goUp();
        //         if (!isPathAlreadyTaken(p, newCl)){
        //             m.setCurLoc(newCl);
        //             r.setUpChild(newCl);
        //             // Console.WriteLine("Atas");
        //         }
        //     }


        //     return coordinates;
        // }

        
        public Stack<Point> solve(Route r, Map m, Stack<Point> p){
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
                    if (isAllTreasureTaken(solve(r.getUpChild(), m, p), m.getTreasureLocations())){
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
                    if (isAllTreasureTaken(solve(r.getLeftChild(), m, p), m.getTreasureLocations())){
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
                    if (isAllTreasureTaken(solve(r.getRightChild(), m, p), m.getTreasureLocations())){
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
                    if (isAllTreasureTaken(solve(r.getDownChild(), m, p), m.getTreasureLocations())){
                        return p;
                    }
                }
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
            // Stack<Point> temp = new Stack<Point>();
            // for(int i = 0; i < tLoc.Length; i++){
            //     bool found = false;
            //     // checking
            //     if (p.Count > 0){
            //         while (p.Count > 0){
            //             Point top = p.Pop();
            //             if (tLoc[i].isTheSame(top)){
            //                 found = true;
            //             }
            //             temp.Push(top);
            //         }
            //     } else {
            //         while (temp.Count > 0){
            //             Point top = temp.Pop();
            //             if(tLoc[i].isTheSame(top)){
            //                 found = true;
            //             }
            //             p.Push(top);
            //         }
            //     }
            //     if (!found){
            //         // give back elements
            //         while (temp.Count > 0){
            //             Point top = temp.Pop();
            //             p.Push(top);
            //         }
            //         return false;
            //     }
            // }
            // // Console.WriteLine("ketemu");
            // while (temp.Count > 0){
            //     Point top = temp.Pop();
            //     p.Push(top);
            // }
            return true;
        }

        public void getSolution(Route r, Map m, Stack<Point> p){
            startTime();
            Stack<Point> solution = solve(r, m, p);

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
    }

}