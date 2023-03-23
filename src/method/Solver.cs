using System;
using System.Diagnostics;
using System.Collections.Generic;
using Class;

namespace Method
{
    public class Solver
    {
        protected int nodes;
        protected int steps;
        protected char[] solRoutes;
        protected Point[] solPaths;
        protected Nodes routeNodes;
        protected Stopwatch watch;
        protected long time;

        // ctor
        public Solver()
        {
            this.nodes = 0;
            this.steps = 0;
            this.solRoutes = new char[] {};
            this.solPaths = new Point[] {};
            this.routeNodes = new Nodes();
            this.watch = new Stopwatch();
            this.time = 0;
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
        public Nodes getRoute(){
            return this.routeNodes;
        }

        // other methods
        public void startTime(){
            this.watch.Start();
        }

        public void stopTime(){
            this.watch.Stop();
        }

        public double getExecutionTime(){
            return this.time / 10000.0;
        }

        public void getInfo(bool displayNodes){
            System.Console.WriteLine("==========================");
            Console.WriteLine("Execution Time: " + getExecutionTime() + " ms");
            Console.WriteLine("Total Nodes: "+ getNodes());
            Console.WriteLine("Total Steps: "+ getSteps());
            Console.Write("Solution Route: ");
            displaySolutionRoutes();
            Console.WriteLine("Solution Paths: ");
            displaySolutionPaths();
            if (displayNodes)
            {
                Console.WriteLine("Constructed Nodes: ");
                routeNodes.displayRoutes(2, 0);
            }
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

        public void copySolutionPathsBFS(Stack<Point> paths){
            this.solPaths = new Point[paths.Count];
            for(int i = solPaths.Length-1 ; i >=0 ; i--){
                Point top = paths.Pop();
                this.solPaths[i] = new Point(top);
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

        public string generateSolutionRoutes()
        {
            string str = "(";
            for (int i = 0; i < this.solRoutes.Length; i++)
            {
                if (i == this.solRoutes.Length - 1)
                {
                    str += this.solRoutes[i];
                }
                else
                {
                    str += this.solRoutes[i] + ", ";
                }
                if ((i+1) % 35 == 0)
                {
                    str += "\n";
                }
            }
            str += ")";
            return str;
        }

        public void displaySolutionPaths(){
            if (solPaths.Length != 0)
            {
                for (int i = 0; i < this.solPaths.Length; i++)
                {
                    if (i % 5 == 0)
                    {
                        Console.Write("(");
                    }

                    if (i == this.solPaths.Length - 1 && i % 5 != 4)
                    {
                        this.solPaths[i].displayPoint();
                        Console.WriteLine(")");
                    }
                    else
                    {
                        if (i % 5 != 4)
                        {
                            this.solPaths[i].displayPoint();
                            Console.Write(" -> ");
                        }
                        else
                        {
                            this.solPaths[i].displayPoint();
                            Console.WriteLine(")");
                        }
                    }
                }
            } else
            {
                Console.WriteLine("No solution is stored.");
            }


        }
        
    }
}