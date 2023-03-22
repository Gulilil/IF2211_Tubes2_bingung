using System

namespace src

    class DFS : Solver
    
        bool TSP

        // ctor
        DFS() 
            this.TSP <- false

        

        // setter getter
        bool getTSP() 
            -> this.TSP
        

        procedure setTSP(bool tsp) 
            this.TSP <- tsp
        

        // other methods

        bool isPathAlreadyTaken(Stack<Point> paths, Point p) 
            -> paths.Contains(p)
        

        Point[] sortPriorityCoordinate(Point[] p, Map m) 
            // using insertion sort
            if (p.Length > 1) 
                for (int i <- 1 i < p.Length i++) 
                    Point temp <- new Point(p[i])
                    int j <- i - 1
                    while (m.getVCAtCoordinate(temp) < m.getVCAtCoordinate(p[j]) and j > 0) 
                        p[j + 1].copyPoint(p[j])
                        j <- j - 1
                    
                    if (m.getVCAtCoordinate(temp) ><- m.getVCAtCoordinate(p[j])) 
                        p[j + 1].copyPoint(temp)
                     else 
                        p[j + 1].copyPoint(p[j])
                        p[j].copyPoint(temp)
                    
                
            
            -> p
        

        Point[] getPriorityCoordinates(Map m) 
            Point[] coordinates <- new Point[]  
            Point cl <- m.getCurLoc()

            if (cl.getRow() !<- 0 and m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) !<- 'X')
            
                // check Up
                Point newCl <- new Point(cl)
                newCl.goUp()
                coordinates <- insertLastPaths(coordinates, newCl)
            
            if (cl.getCol() !<- 0 and m.getValueAtCoordinate(cl.getRow(), cl.getCol() - 1) !<- 'X')
            
                // check Left
                Point newCl <- new Point(cl)
                newCl.goLeft()
                coordinates <- insertLastPaths(coordinates, newCl)
            
            if (cl.getCol() !<- m.getCol() - 1 and m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) !<- 'X')
            
                // check Right
                Point newCl <- new Point(cl)
                newCl.goRight()
                coordinates <- insertLastPaths(coordinates, newCl)
            
            if (cl.getRow() !<- m.getRow() - 1 and m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) !<- 'X')
            
                // check Down
                Point newCl <- new Point(cl)
                newCl.goDown()
                coordinates <- insertLastPaths(coordinates, newCl)
            
            coordinates <- sortPriorityCoordinate(coordinates, m)
            -> coordinates
        

        Nodes setNewNodes(Point cl, Point newCl, Nodes n) 
            if (newCl.isUpOf(cl)) 
                n.setUpChild(newCl)
                -> n.getUpChild()
             else if (newCl.isLeftOf(cl)) 
                n.setLeftChild(newCl)
                -> n.getLeftChild()
             else if (newCl.isRightOf(cl)) 
                n.setRightChild(newCl)
                -> n.getRightChild()
             else if (newCl.isDownOf(cl)) 
                n.setDownChild(newCl)
                -> n.getDownChild()
            
            -> n
        


        Stack<Point> solve(Nodes n, Map m, Stack<Point> p)
        
            Point cl <- m.getCurLoc()
            p.Push(cl)
            m.increaseVCAtCoordinate(cl)

            bool objectives
            if (this.TSP)
            
                objectives <- isAllTreasureTaken(p, m.getTreasureLocations()) and p.Peek().Equals(m.getStartLoc())
             else
            
                objectives <- isAllTreasureTaken(p, m.getTreasureLocations())
            

            if (objectives)
            
                -> p
            

            int iteration <- 0
            Point[] availableCoordinates <- getPriorityCoordinates(m)
            while (iteration < availableCoordinates.Length)
            
                m.setCurLoc(availableCoordinates[iteration])
                Nodes newNodes <- setNewNodes(cl, availableCoordinates[iteration], n)
                if (isAllTreasureTaken(solve(newNodes, m, p), m.getTreasureLocations()))
                
                    -> p
                
                iteration++
            

            // backtrack
            Point dump <- p.Pop()
            -> new Stack<Point>()
        


        Stack<Point> solveOneWay(Nodes n, Map m, Stack<Point> p)
        
            Point cl <- m.getCurLoc()
            n.setNode(cl)
            p.Push(n.getNode())

            if (isAllTreasureTaken(p, m.getTreasureLocations()))
            
                -> p
            

            if (cl.getRow() !<- 0 and m.getValueAtCoordinate(cl.getRow() - 1, cl.getCol()) !<- 'X')
            
                // check Up
                Point newCl <- new Point(cl)
                newCl.goUp()
                if (!isPathAlreadyTaken(p, newCl))
                
                    m.setCurLoc(newCl)
                    n.setUpChild(newCl)
                    if (isAllTreasureTaken(solveOneWay(n.getUpChild(), m, p), m.getTreasureLocations()))
                    
                        -> p
                    
                
            
            if (cl.getCol() !<- 0 and m.getValueAtCoordinate(cl.getRow(), cl.getCol() - 1) !<- 'X')
            
                // check Left
                Point newCl <- new Point(cl)
                newCl.goLeft()
                if (!isPathAlreadyTaken(p, newCl))
                
                    m.setCurLoc(newCl)
                    n.setLeftChild(newCl)
                    if (isAllTreasureTaken(solveOneWay(n.getLeftChild(), m, p), m.getTreasureLocations()))
                    
                        -> p
                    
                
            
            if (cl.getCol() !<- m.getCol() - 1 and m.getValueAtCoordinate(cl.getRow(), cl.getCol() + 1) !<- 'X')
            
                // check Right
                Point newCl <- new Point(cl)
                newCl.goRight()
                if (!isPathAlreadyTaken(p, newCl))
                
                    m.setCurLoc(newCl)
                    n.setRightChild(newCl)
                    if (isAllTreasureTaken(solveOneWay(n.getRightChild(), m, p), m.getTreasureLocations()))
                    
                        -> p
                    
                
            
            if (cl.getRow() !<- m.getRow() - 1 and m.getValueAtCoordinate(cl.getRow() + 1, cl.getCol()) !<- 'X')
            
                // check Down
                Point newCl <- new Point(cl)
                newCl.goDown()
                if (!isPathAlreadyTaken(p, newCl))
                
                    m.setCurLoc(newCl)
                    n.setDownChild(newCl)
                    if (isAllTreasureTaken(solveOneWay(n.getDownChild(), m, p), m.getTreasureLocations()))
                    
                        -> p
                    
                
            
            // backtrack
            Point dump <- p.Pop()
            -> new Stack<Point>()
        


        bool isAllTreasureTaken(Stack<Point> p, Point[] tLoc) 
            for (int i <- 0 i < tLoc.Length i++) 
                if (!p.Contains(tLoc[i])) 
                    -> false
                
            
            -> true
        

        procedure getSolution(Map m) 
            if (m.getValid())
            
                Stack<Point> p <- new Stack<Point>()
                Stack<Point> solution <- new Stack<Point>()
                if (!this.TSP)
                
                    startTime()
                    solution <- solveOneWay(routeNodes, m, p)
                    stopTime()
                
                if (this.TSP or solution.Count = 0)
                
                    m.resetMap()
                    routeNodes <- new Nodes()
                    startTime()
                    solution <- solve(routeNodes, m, p)
                    stopTime()
                

                this.steps <- solution.Count
                this.nodes <- routeNodes.getNodesAmount()
                copySolutionPathsDFS(solution)
                convertPathsToRoutes()
             else
            
                output("DFS method cannot be done since the map is invalid.")
            
        
    


