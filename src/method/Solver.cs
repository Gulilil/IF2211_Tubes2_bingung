using System;
using System.Diagnostics;

namespace src
{
    public class Solver
    {
        private int nodes;
        private int steps;
        private char[] routes;
        private Point[] paths;
        private Stopwatch watch;

        // ctor
        public Solver()
        {
            this.nodes = 0;
            this.steps = 0;
            this.routes = new char[] {};
            this.paths = new Point[] {};
            this.watch = new Stopwatch();
        }

        // public Solver(int n, int s, char[] r, Point[] p){
        //     this.nodes = n;
        //     this.steps = s;
        //     int i;
        //     this.routes = new char[r.Length];
        //     for (i = 0; i < r.Length; i++){
        //         this.routes[i] = r[i];
        //     }
        //     this.paths = new Point[p.Length];
        //     for(i = 0; i < p.Length; i++){
        //         this.paths[i] = p[i];
        //     }
        //     this.watch = new Stopwatch();
        // }

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
        public char[] getRoutes()
        {
            return this.routes;
        }
        public Point[] getPaths()
        {
            return this.paths;
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

        public char[] insertLastRoutes(char[] routes, char c){
            char[] temp = (char[])routes.Clone();
            routes = new char[temp.Length+1];
            for (int i = 0; i < routes.Length; i++){
                if(i == routes.Length-1){
                    routes[i] = c;
                } else {
                    routes[i] = temp[i];
                }
            }
            return routes;
        }

        public char[] deleteFirstRoutes(char[] routes){
            char[] temp = (char[])routes.Clone();
            routes = new char[temp.Length-1];
            for (int i = 0; i < routes.Length; i++){
                routes[i] = temp[i+1];
            }
            return routes;
        }

        public Point[] insertFirstPaths(Point[] paths, Point p)
        {
            Point[] temp = (Point[])paths.Clone();
            paths = new Point[temp.Length+1];
            for (int i = 0; i < paths.Length; i++){
                if (i == 0){
                    paths[i] = p;
                } else {
                    paths[i] = temp[i-1];
                }
            }
            return paths;
        }

        public Point[] insertLastPaths(Point[] paths, Point p)
        {
            Point[] temp = (Point[])paths.Clone();
            paths = new Point[temp.Length+1];
            for (int i = 0; i < paths.Length; i++){
                if (i == paths.Length-1){
                    paths[i] = p;
                } else {
                    paths[i] = temp[i];
                }
            }
            return paths;
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
            this.routes = new char[this.paths.Length-1];
            for (int i = 0; i < this.paths.Length-1; i++){
                if (this.paths[i+1].isUpOf(this.paths[i])){
                    this.routes = this.insertLastRoutes(this.routes, 'U');
                } else if (this.paths[i+1].isDownOf(this.paths[i])){
                    this.routes = this.insertLastRoutes(this.routes, 'D');
                } else if (this.paths[i+1].isLeftOf(this.paths[i])){
                    this.routes = this.insertLastRoutes(this.routes, 'L');
                } else if (this.paths[i+1].isRightOf(this.paths[i])){
                    this.routes = this.insertLastRoutes(this.routes, 'R');
                }
            }
        }
    }
}