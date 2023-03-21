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

        public Stack<Point> solve(Route r, Map m){
            Queue<Stack<Point>> q = new Queue<Stack<Point>>(); 
            Queue<Route> rQueue = new Queue<Route>();
            Stack<Point> s = new Stack<Point>();
            s.Push(m.getStartLoc());
            q.Enqueue(s);
            r.setNode(m.getStartLoc());
            rQueue.Enqueue(r);
            //int count = 1;
            while (q.Count > 0 && !isAllTreasureTaken(q.Peek(), m.getTreasureLocations())){
                r = rQueue.Dequeue();
                Stack<Point> temp = q.Dequeue();
                m.setCurLoc(temp.Peek());
                Point cl = m.getCurLoc();
                m.increaseVCAtCoordinate(cl);
                bool noOtherPath = true;
                for(int i = 0; i < 3; i++){
                    if (noOtherPath){
                        if (i == 2 && noOtherPath)
                        {
                            return new Stack<Point>();
                        }
                        if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
                        {
                            // check Up
                            Point newLoc = new Point(cl);
                            newLoc.goUp();
                            if (!temp.Contains(newLoc) || (i == 1 && noOtherPath)){
                                Stack<Point> temp2 = new Stack<Point>(temp);
                                Stack<Point> temp1 = new Stack<Point>(temp2);
                                temp1.Push(newLoc);
                                r.setUpChild(newLoc);
                                rQueue.Enqueue(r.getUpChild());
                                q.Enqueue(temp1);
                                noOtherPath = false;
                            }
                        }
                        if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()-1) != 'X')
                        {
                            // check Left
                            Point newLoc = new Point(cl);
                            newLoc.goLeft();
                            if (!temp.Contains(newLoc) || (i == 1 && noOtherPath)){
                                Stack<Point> temp2 = new Stack<Point>(temp);
                                Stack<Point> temp1 = new Stack<Point>(temp2);
                                temp1.Push(newLoc);
                                r.setLeftChild(newLoc);
                                rQueue.Enqueue(r.getLeftChild());
                                q.Enqueue(temp1);
                                noOtherPath = false;
                            }
                        }
                        if (cl.getCol() != m.getCol() - 1 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) != 'X')
                        {
                            // check Right
                            Point newLoc = new Point(cl);
                            newLoc.goRight();
                            if (!temp.Contains(newLoc) || (i == 1 && noOtherPath)){
                                Stack<Point> temp2 = new Stack<Point>(temp);
                                Stack<Point> temp1 = new Stack<Point>(temp2);
                                temp1.Push(newLoc);
                                r.setRightChild(newLoc);
                                rQueue.Enqueue(r.getRightChild());
                                q.Enqueue(temp1);
                                noOtherPath = false;
                            }
                        }
                        if (cl.getRow() != m.getRow() - 1 && m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) != 'X')
                        {
                            // check Down
                            Point newLoc = new Point(cl);
                            newLoc.goDown();
                            if (!temp.Contains(newLoc) || (i == 1 && noOtherPath)){
                                Stack<Point> temp2 = new Stack<Point>(temp);
                                Stack<Point> temp1 = new Stack<Point>(temp2);
                                temp1.Push(newLoc);
                                r.setDownChild(newLoc);
                                rQueue.Enqueue(r.getDownChild());
                                q.Enqueue(temp1);
                                noOtherPath = false;
                            }
                        }
                    }
                }
                //count++;
            }
            if (TSP){
                r = rQueue.Dequeue();
                while(rQueue.Count > 0)
                {
                    rQueue.Dequeue();
                }
                rQueue.Enqueue(r);
                Stack<Point> sol = q.Dequeue();
                while (q.Count > 0){
                    q.Dequeue();
                }
                q.Enqueue(sol);
                while(!q.Peek().Peek().Equals(m.getStartLoc())){
                    r = rQueue.Dequeue();
                    Stack<Point> temp = q.Dequeue();
                    m.setCurLoc(temp.Peek());
                    Point cl = m.getCurLoc();
                    if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
                    {
                        // check Up
                        Point newLoc = new Point(cl);
                        newLoc.goUp();
                        Stack<Point> temp2 = new Stack<Point>(temp);
                        Stack<Point> temp1 = new Stack<Point>(temp2);
                        r.setUpChild(newLoc);
                        rQueue.Enqueue(r);
                        temp1.Push(newLoc);
                        q.Enqueue(temp1);
                    }
                    if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol()-1) != 'X')
                    {
                        // check Left
                        Point newLoc = new Point(cl);
                        newLoc.goLeft();
                        Stack<Point> temp2 = new Stack<Point>(temp);
                        Stack<Point> temp1 = new Stack<Point>(temp2);
                        r.setLeftChild(newLoc);
                        rQueue.Enqueue(r);
                        temp1.Push(newLoc);
                        q.Enqueue(temp1);
                    }
                    if (cl.getCol() != m.getCol() - 1 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) != 'X')
                    {
                        // check Right
                        Point newLoc = new Point(cl);
                        newLoc.goRight();
                        Stack<Point> temp2 = new Stack<Point>(temp);
                        Stack<Point> temp1 = new Stack<Point>(temp2);
                        r.setRightChild(newLoc);
                        rQueue.Enqueue(r);
                        temp1.Push(newLoc);
                        q.Enqueue(temp1);
                    }
                    if (cl.getRow() != m.getRow() - 1 && m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) != 'X')
                    {
                        // check Down
                        Point newLoc = new Point(cl);
                        newLoc.goDown();
                        Stack<Point> temp2 = new Stack<Point>(temp);
                        Stack<Point> temp1 = new Stack<Point>(temp2);
                        r.setDownChild(newLoc);
                        rQueue.Enqueue(r);
                        temp1.Push(newLoc);
                        q.Enqueue(temp1);
                    }
                    //count++;
                }
            }
            if (q.Count > 0){
                //setNodes(count - 1);
                return q.Dequeue();
            }
            return new Stack<Point>();
        }

        public void getSolution(Map m){
            if (m.getValid())
            {
                startTime();
                Stack<Point> solution = solve(routeNodes, m);
                stopTime();

                this.nodes = routeNodes.getNodesAmount();
                this.steps = solution.Count;
                copySolutionPathsBFS(solution);
                convertPathsToRoutes();
            } else
            {
                Console.WriteLine("BFS method cannot be done since the map is invalid.");
            }
        }
    }
}