using System;
using System.Diagnostics;

namespace src
{
    public class Solver
    {
        protected int nodes;
        protected int steps;
        protected char[] solRoutes;
        protected Point[] solPaths;
        protected Route routeNodes;
        protected Stopwatch watch;

        // ctor
        public Solver()
        {
            this.nodes = 0;
            this.steps = 0;
            this.solRoutes = new char[] {};
            this.solPaths = new Point[] {};
            this.routeNodes = new Route();
            this.watch = new Stopwatch();
        }

        // setter getter
        public void setNodes (int n){
            this.nodes = n;
        }
        public void setSteps (int s){
            this.steps = s;
        }
        public int getNodes(){
            return this.nodes;
        }
        public int getSteps(){
            return this.steps;
        }
        public char[] getSolRoutes()
        {
            return this.solRoutes;
        }
        public Point[] getSolPaths()
        {
            return this.solPaths;
        }
        public Route getRoute(){
            return this.routeNodes;
        }

        // other methods
        public void startTime(){
            this.watch.Start();
        }

        public void stopTime(){
            this.watch.Stop();
        }

        public long getExecutionTime(){
            return this.watch.ElapsedMilliseconds;
        }

        public void getInfo(){
            System.Console.WriteLine("==========================");
            Console.WriteLine("Execution Time: " + getExecutionTime() + " ms");
            Console.WriteLine("Total Nodes: "+ getNodes());
            Console.WriteLine("Total Steps: "+ getSteps());
            Console.Write("Solution Route: ");
            displaySolutionRoutes();
            Console.WriteLine("Solution Paths: ");
            displaySolutionPaths();
            Console.WriteLine("Constructed Nodes: ");
            routeNodes.displayRoutes(2,0);
        }

        public char[] insertLastRoutes(char[] routes, char c){
            char[] temp = new char[routes.Length+1];
            for (int i = 0; i < temp.Length; i++){
                if(i == temp.Length-1){
                    temp[i] = c;
                } else {
                    temp[i] = routes[i];
                }
            }
            return temp;
        }

        public char[] deleteFirstRoutes(char[] routes){
            char[] temp = (char[])routes.Clone();
            routes = new char[temp.Length-1];
            for (int i = 0; i < routes.Length; i++){
                routes[i] = temp[i+1];
            }
            return routes;
        }

        // public Point[] insertFirstPaths(Point[] paths, Point p)
        // {
        //     Point[] temp = (Point[])paths.Clone();
        //     paths = new Point[temp.Length+1];
        //     for (int i = 0; i < paths.Length; i++){
        //         if (i == 0){
        //             paths[i] = p;
        //         } else {
        //             paths[i] = temp[i-1];
        //         }
        //     }
        //     return paths;
        // }

        public Point[] insertLastPaths(Point[] paths, Point p)
        {
            Point[] temp = new Point[paths.Length+1];
            for (int i = 0; i < temp.Length; i++){
                if (i == temp.Length-1){
                    temp[i] = p;
                } else {
                    temp[i] = paths[i];
                }
            }
            return temp;
        }

        public Point[] deleteFirstPaths(Point[] paths)
        {
            Point[] temp = (Point[])paths.Clone();
            paths = new Point[temp.Length-1];
            for (int i = 0; i < paths.Length; i++){
                paths[i] = temp[i+1];
            }
            return paths;
        }

        public void convertPathsToRoutes(){
            for (int i = 0; i < this.solPaths.Length-1; i++){
                if (this.solPaths[i+1].isUpOf(this.solPaths[i])){
                    this.solRoutes = this.insertLastRoutes(this.solRoutes, 'U');
                } else if (this.solPaths[i+1].isDownOf(this.solPaths[i])){
                    this.solRoutes = this.insertLastRoutes(this.solRoutes, 'D');
                } else if (this.solPaths[i+1].isLeftOf(this.solPaths[i])){
                    this.solRoutes = this.insertLastRoutes(this.solRoutes, 'L');
                } else if (this.solPaths[i+1].isRightOf(this.solPaths[i])){
                    this.solRoutes = this.insertLastRoutes(this.solRoutes, 'R');
                }
            }
        }

        public void copySolutionPathsDFS(Stack<Point> paths){
            this.solPaths = new Point[paths.Count];
            for(int i = solPaths.Length-1 ; i >=0 ; i--){
                Point top = paths.Pop();
                this.solPaths[i] = new Point(top);
            }
            for (int i = 0; i < solPaths.Length; i++){
                paths.Push(solPaths[i]);
            }
        }


        // print and display
        public void displaySolutionRoutes(){
            Console.Write("(");
            for(int i = 0; i < this.solRoutes.Length; i++){
                if (i == this.solRoutes.Length-1){
                    Console.Write(this.solRoutes[i]);
                } else {
                    Console.Write(this.solRoutes[i]+", ");
                }
            }
            Console.WriteLine(")");
        }

        public void displaySolutionPaths(){
            for(int i = 0; i < this.solPaths.Length;i++){
                if (i % 5 == 0){
                    Console.Write("(");
                } 

                if (i == this.solPaths.Length -1 && i % 5 != 4){
                    this.solPaths[i].displayPoint();
                    Console.WriteLine(")");
                } else {
                    if (i % 5 != 4){
                        this.solPaths[i].displayPoint();
                        Console.Write(" -> ");
                    } else {
                        this.solPaths[i].displayPoint();
                        Console.WriteLine(")");
                    }
                }

            }
        }
        
    }
}