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

        public void insertLastRoutes(char c){
            char[] temp = (char[])this.routes.Clone();
            this.routes = new char[temp.Length+1];
            for (int i = 0; i < this.routes.Length; i++){
                if(i == this.routes.Length-1){
                    this.routes[i] = c;
                } else {
                    this.routes[i] = temp[i];
                }
            }
        }

        public void deleteFirstRoutes(){
            char[] temp = (char[])this.routes.Clone();
            this.routes = new char[temp.Length-1];
            for (int i = 0; i < this.routes.Length; i++){
                this.routes[i] = temp[i+1];
            }
        }

        public void insertFirstPaths(Point p)
        {
            Point[] temp = (Point[])this.paths.Clone();
            this.paths = new Point[temp.Length+1];
            for (int i = 0; i < this.paths.Length; i++){
                if (i == 0){
                    this.paths[i] = p;
                } else {
                    this.paths[i] = temp[i-1];
                }
            }
        }

        public void insertLastPaths(Point p)
        {
            Point[] temp = (Point[])this.paths.Clone();
            this.paths = new Point[temp.Length+1];
            for (int i = 0; i < this.paths.Length; i++){
                if (i == this.paths.Length){
                    this.paths[i] = p;
                } else {
                    this.paths[i] = temp[i];
                }
            }
        }

        public void deleteFirstPaths()
        {
            Point[] temp = (Point[])this.paths.Clone();
            this.paths = new Point[temp.Length-1];
            for (int i = 0; i < this.paths.Length; i++){
                this.paths[i] = temp[i+1];
            }
        }

        public void convertPathsToRoutes(){
            this.routes = new char[this.paths.Length-1];
            for (int i = 0; i < this.paths.Length-1; i++){
                if (this.paths[i+1].isUpOf(this.paths[i])){
                    this.insertLastRoutes('U');
                } else if (this.paths[i+1].isDownOf(this.paths[i])){
                    this.insertLastRoutes('D');
                } else if (this.paths[i+1].isLeftOf(this.paths[i])){
                    this.insertLastRoutes('L');
                } else if (this.paths[i+1].isRightOf(this.paths[i])){
                    this.insertLastRoutes('R');
                }
            }
        }
    }
}