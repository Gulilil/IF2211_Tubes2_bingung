using System;

namespace src
{
    public class Program
    {
        public static void Main(string[] args){
            Map map = new Map();
            DFS dfs = new DFS();
            Route route = new Route();
            Stack<Point> stackPath = new Stack<Point> ();

            map.ReadFile();
            map.getInfo();

            dfs.getSolution(route, map, stackPath);
            Console.WriteLine("Execution Time: " + dfs.getExecutionTime() + " ms");
            Console.WriteLine("Total Nodes: "+ dfs.getNodes());
            Console.WriteLine("Total Steps: "+ dfs.getSteps());
            Console.Write("Solution Route: ");
            dfs.displaySolutionRoutes();
            Console.WriteLine("Solution Paths: ");
            dfs.displaySolutionPaths();

            Console.WriteLine("==========================");
            Console.WriteLine("==========================");
            

        }

    }
}
