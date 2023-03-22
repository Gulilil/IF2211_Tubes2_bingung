using System
using Diagnostics

namespace src

class Solver

int nodes
int steps
char[] solRoutes
Point[] solPaths
Nodes routeNodes
Stopwatch watch

// ctor
Solver()

    this.nodes <- 0
    this.steps <- 0
    this.solRoutes <- new char[] 
    this.solPaths <- new Point[] 
    this.routeNodes <- new Nodes()
    this.watch <- new Stopwatch()


// setter getter
procedure setNodes (int n)
    this.nodes <- n

procedure setSteps (int s)
    this.steps <- s

int getNodes()
    -> this.nodes

int getSteps()
    -> this.steps

char[] getSolRoutes()

    -> this.solRoutes

Point[] getSolPaths()

    -> this.solPaths

Nodes getRoute()
    -> this.routeNodes


// other methods
procedure startTime()
    this.watch.Start()


procedure stopTime()
    this.watch.Stop()


long getExecutionTime()
    -> this.watch.ElapsedMilliseconds


procedure getInfo(bool displayNodes)
    output("=============")
    output("Execution Time: " + getExecutionTime() + " ms")
    output("Total Nodes: "+ getNodes())
    output("Total Steps: "+ getSteps())
    output("Solution Route: ")
    displaySolutionRoutes()
    output("Solution Paths: ")
    displaySolutionPaths()
    if (displayNodes)
    
        output("Constructed Nodes: ")
        routeNodes.displayRoutes(2, 0)
    


char[] insertLastRoutes(char[] routes, char c)
    char[] temp <- new char[routes.Length+1]
    for (int i <- 0 i < temp.Length i++)
        if(i = temp.Length-1)
            temp[i] <- c
            else 
            temp[i] <- routes[i]
        
    
    -> temp


char[] deleteFirstRoutes(char[] routes)
    char[] temp <- (char[])routes.Clone()
    routes <- new char[temp.Length-1]
    for (int i <- 0 i < routes.Length i++)
        routes[i] <- temp[i+1]
    
    -> routes


Point[] insertLastPaths(Point[] paths, Point p)

    Point[] temp <- new Point[paths.Length+1]
    for (int i <- 0 i < temp.Length i++)
        if (i = temp.Length-1)
            temp[i] <- p
            else 
            temp[i] <- paths[i]
        
    
    -> temp


Point[] deleteFirstPaths(Point[] paths)

    Point[] temp <- (Point[])paths.Clone()
    paths <- new Point[temp.Length-1]
    for (int i <- 0 i < paths.Length i++)
        paths[i] <- temp[i+1]
    
    -> paths


procedure convertPathsToRoutes()
    for (int i <- 0 i < this.solPaths.Length-1 i++)
        if (this.solPaths[i+1].isUpOf(this.solPaths[i]))
            this.solRoutes <- this.insertLastRoutes(this.solRoutes, 'U')
            else if (this.solPaths[i+1].isDownOf(this.solPaths[i]))
            this.solRoutes <- this.insertLastRoutes(this.solRoutes, 'D')
            else if (this.solPaths[i+1].isLeftOf(this.solPaths[i]))
            this.solRoutes <- this.insertLastRoutes(this.solRoutes, 'L')
            else if (this.solPaths[i+1].isRightOf(this.solPaths[i]))
            this.solRoutes <- this.insertLastRoutes(this.solRoutes, 'R')
        
    


procedure copySolutionPathsDFS(Stack<Point> paths)
    this.solPaths <- new Point[paths.Count]
    for(int i <- solPaths.Length-1  i ><-0  i--)
        Point top <- paths.Pop()
        this.solPaths[i] <- new Point(top)
    
    for (int i <- 0 i < solPaths.Length i++)
        paths.Push(solPaths[i])
    


procedure copySolutionPathsBFS(Stack<Point> paths)
    this.solPaths <- new Point[paths.Count]
    for(int i <- solPaths.Length-1  i ><-0  i--)
        Point top <- paths.Pop()
        this.solPaths[i] <- new Point(top)
    



// print and display
procedure displaySolutionRoutes()
    output("(")
    for(int i <- 0 i < this.solRoutes.Length i++)
        if (i = this.solRoutes.Length-1)
            output(this.solRoutes[i])
            else 
            output(this.solRoutes[i]+", ")
        
    
    output(")")


procedure displaySolutionPaths()
    if (solPaths.Length !<- 0)
    
        for (int i <- 0 i < this.solPaths.Length i++)
        
            if (i % 5 = 0)
            
                output("(")
            

            if (i = this.solPaths.Length - 1 and i % 5 !<- 4)
            
                this.solPaths[i].displayPoint()
                output(")")
            
            else
            
                if (i % 5 !<- 4)
                
                    this.solPaths[i].displayPoint()
                    output(" -> ")
                
                else
                
                    this.solPaths[i].displayPoint()
                    output(")")
                
            
        
        else
    
        output("No solution is stored.")
    





