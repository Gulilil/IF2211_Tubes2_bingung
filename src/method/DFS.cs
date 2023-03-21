using System;

namespace src
{
    public class DFS : Solver
    {
        private bool TSP;

        // ctor
        public DFS() {
            this.TSP = false;

        }

        // setter getter
        public bool getTSP() {
            return this.TSP;
        }

        public void setTSP(bool tsp) {
            this.TSP = tsp;
        }

        // other methods

        public bool isPathAlreadyTaken(Stack<Point> paths, Point p) {
            return paths.Contains(p);
        }

        public Point[] sortPriorityCoordinate(Point[] p, Map m) {
            // using insertion sort
            if (p.Length > 1) {
                for (int i = 1; i < p.Length; i++) {
                    Point temp = new Point(p[i]);
                    int j = i - 1;
                    while (m.getVCAtCoordinate(temp) < m.getVCAtCoordinate(p[j]) && j > 0) {
                        p[j + 1].copyPoint(p[j]);
                        j = j - 1;
                    }
                    if (m.getVCAtCoordinate(temp) >= m.getVCAtCoordinate(p[j])) {
                        p[j + 1].copyPoint(temp);
                    } else {
                        p[j + 1].copyPoint(p[j]);
                        p[j].copyPoint(temp);
                    }
                }
            }
            return p;
        }

        public Point[] getPriorityCoordinates(Map m) {
            Point[] coordinates = new Point[] { };
            Point cl = m.getCurLoc();

            if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
            {
                // check Up
                Point newCl = new Point(cl);
                newCl.goUp();
                coordinates = insertLastPaths(coordinates, newCl);
            }
            if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() - 1) != 'X')
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
            coordinates = sortPriorityCoordinate(coordinates, m);
            return coordinates;
        }

        public Route setNewRoutes(Point cl, Point newCl, Route r) {
            if (newCl.isUpOf(cl)) {
                r.setUpChild(newCl);
                return r.getUpChild();
            } else if (newCl.isLeftOf(cl)) {
                r.setLeftChild(newCl);
                return r.getLeftChild();
            } else if (newCl.isRightOf(cl)) {
                r.setRightChild(newCl);
                return r.getRightChild();
            } else if (newCl.isDownOf(cl)) {
                r.setDownChild(newCl);
                return r.getDownChild();
            }
            return r;
        }


        public Stack<Point> solve(Route r, Map m, Stack<Point> p)
        {
            Point cl = m.getCurLoc();
            p.Push(cl);
            m.increaseVCAtCoordinate(cl);

            bool objectives;
            if (this.TSP)
            {
                objectives = isAllTreasureTaken(p, m.getTreasureLocations()) && p.Peek().Equals(m.getStartLoc());
            } else
            {
                objectives = isAllTreasureTaken(p, m.getTreasureLocations());
            }

            if (objectives)
            {
                return p;
            }

            int iteration = 0;
            Point[] availableCoordinates = getPriorityCoordinates(m);
            while (iteration < availableCoordinates.Length)
            {
                m.setCurLoc(availableCoordinates[iteration]);
                Route newRoute = setNewRoutes(cl, availableCoordinates[iteration], r);
                if (isAllTreasureTaken(solve(newRoute, m, p), m.getTreasureLocations()))
                {
                    return p;
                }
                iteration++;
            }

            // backtrack
            Point dump = p.Pop();
            return new Stack<Point>();
        }


        public Stack<Point> solveOneWay(Route r, Map m, Stack<Point> p)
        {
            Point cl = m.getCurLoc();
            r.setNode(cl);
            p.Push(r.getNode());

            if (isAllTreasureTaken(p, m.getTreasureLocations()))
            {
                return p;
            }

            if (cl.getRow() != 0 && m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) != 'X')
            {
                // check Up
                Point newCl = new Point(cl);
                newCl.goUp();
                if (!isPathAlreadyTaken(p, newCl))
                {
                    m.setCurLoc(newCl);
                    r.setUpChild(newCl);
                    if (isAllTreasureTaken(solveOneWay(r.getUpChild(), m, p), m.getTreasureLocations()))
                    {
                        return p;
                    }
                }
            }
            if (cl.getCol() != 0 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() - 1) != 'X')
            {
                // check Left
                Point newCl = new Point(cl);
                newCl.goLeft();
                if (!isPathAlreadyTaken(p, newCl))
                {
                    m.setCurLoc(newCl);
                    r.setLeftChild(newCl);
                    if (isAllTreasureTaken(solveOneWay(r.getLeftChild(), m, p), m.getTreasureLocations()))
                    {
                        return p;
                    }
                }
            }
            if (cl.getCol() != m.getCol() - 1 && m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) != 'X')
            {
                // check Right
                Point newCl = new Point(cl);
                newCl.goRight();
                if (!isPathAlreadyTaken(p, newCl))
                {
                    m.setCurLoc(newCl);
                    r.setRightChild(newCl);
                    if (isAllTreasureTaken(solveOneWay(r.getRightChild(), m, p), m.getTreasureLocations()))
                    {
                        return p;
                    }
                }
            }
            if (cl.getRow() != m.getRow() - 1 && m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) != 'X')
            {
                // check Down
                Point newCl = new Point(cl);
                newCl.goDown();
                if (!isPathAlreadyTaken(p, newCl))
                {
                    m.setCurLoc(newCl);
                    r.setDownChild(newCl);
                    if (isAllTreasureTaken(solveOneWay(r.getDownChild(), m, p), m.getTreasureLocations()))
                    {
                        return p;
                    }
                }
            }
            // backtrack
            Point dump = p.Pop();
            return new Stack<Point>();
        }


        public bool isAllTreasureTaken(Stack<Point> p, Point[] tLoc) {
            for (int i = 0; i < tLoc.Length; i++) {
                if (!p.Contains(tLoc[i])) {
                    return false;
                }
            }
            return true;
        }

        public void getSolution(Map m) {
            startTime();
            Stack<Point> p = new Stack<Point>();
            Stack<Point> solution = new Stack<Point>();
            if (!this.TSP)
            {
                solution = solveOneWay(routeNodes, m, p);
            }
            m.resetMap();
            if (this.TSP || solution.Count == 0)
            {
                solution = solve(routeNodes, m, p);
            }

            this.steps = solution.Count;
            this.nodes = routeNodes.getNodesAmount();
            copySolutionPathsDFS(solution);
            convertPathsToRoutes();
            stopTime();
        }
    }


}