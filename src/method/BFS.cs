using System.Drawing;
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
        public bool isAllTreasureTaken(Stack<Point> path, Point[] tLoc){
            for (int i = 0; i < tLoc.Length; i++){
                if (!path.Contains(tLoc[i])){
                    return false;
                }
            }
            return true;
        }

        public Stack<Point> solve(Map m){
            Queue<Stack<Point>> q = new Queue<Stack<Point>>(); 
            Stack<Point> s = new Stack<Point>();
            s.Push(m.getStartLoc());
            q.Enqueue(s);
            int count = 1;
            while (q.Count > 0 && !isAllTreasureTaken(q.Peek(), m.getTreasureLocations())){
                Stack<Point> temp = q.Dequeue();
                // System.Console.WriteLine(count);
                m.setCurLoc(temp.Peek());
                Point cl = m.getCurLoc();
                displayPath(temp);
                // m.increaseVCAtCoordinate(cl);
                bool noOtherPath = true;
                for(int i = 0; i < 2; i++){
                    if (noOtherPath){
                        if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
                        {
                            // check Up
                            // System.Console.WriteLine("Up");
                            Point newLoc = new Point(cl);
                            newLoc.goUp();
                            if (!temp.Contains(newLoc) || (i == 1 && noOtherPath)){
                                Stack<Point> temp2 = new Stack<Point>(temp);
                                Stack<Point> temp1 = new Stack<Point>(temp2);
                                temp1.Push(newLoc);
                                q.Enqueue(temp1);
                                noOtherPath = false;
                            }
                        }
                        if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()-1) != 'X')
                        {
                            // check Left
                            // System.Console.WriteLine("Left");
                            Point newLoc = new Point(cl);
                            newLoc.goLeft();
                            if (!temp.Contains(newLoc) || (i == 1 && noOtherPath)){
                                Stack<Point> temp2 = new Stack<Point>(temp);
                                Stack<Point> temp1 = new Stack<Point>(temp2);
                                temp1.Push(newLoc);
                                q.Enqueue(temp1);
                                noOtherPath = false;
                            }
                        }
                        if (cl.getCol() != m.getCol() - 1 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) != 'X')
                        {
                            // check Right
                            // System.Console.WriteLine("Right");
                            Point newLoc = new Point(cl);
                            newLoc.goRight();
                            if (!temp.Contains(newLoc) || (i == 1 && noOtherPath)){
                                Stack<Point> temp2 = new Stack<Point>(temp);
                                Stack<Point> temp1 = new Stack<Point>(temp2);
                                temp1.Push(newLoc);
                                q.Enqueue(temp1);
                                noOtherPath = false;
                                // temp.Pop();
                                // System.Console.WriteLine(temp.Count);
                            }
                        }
                        if (cl.getRow() != m.getRow() - 1 && m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) != 'X')
                        {
                            // check Down
                            // System.Console.WriteLine("Down");
                            Point newLoc = new Point(cl);
                            newLoc.goDown();
                            if (!temp.Contains(newLoc) || (i == 1 && noOtherPath)){
                                Stack<Point> temp2 = new Stack<Point>(temp);
                                Stack<Point> temp1 = new Stack<Point>(temp2);
                                temp1.Push(newLoc);
                                q.Enqueue(temp1);
                                noOtherPath = false;
                                // temp.Pop();
                            }
                        }
                    }
                }
                count++;
                System.Console.WriteLine();
            }
            if (q.Count > 0){
                setNodes(count - 1);
                return q.Dequeue();
            }
            return new Stack<Point>();
        }

        public void getSolution(Map m){
            startTime();
            Stack<Point> solution = solve(m);
            // displayPath(solution);

            this.steps = solution.Count;
            copySolutionPathsBFS(solution);
            convertPathsToRoutes();
            stopTime();
        }

        // print and display
        public void displayPath(Stack<Point> paths)
        {
            // Stack<Point> temp = new Stack<Point>();
            // while (paths.Count > 0)
            // {
            //     Point top = paths.Pop();
            //     top.displayPoint();
            //     temp.Push(top);
            // }
            // while (temp.Count > 0)
            // {
            //     Point top = temp.Pop();
            //     paths.Push(top);
            // }
            foreach (Point element in paths){
                element.displayPoint();
            }
        }
    }
}