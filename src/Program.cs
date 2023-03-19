using System;

namespace src
{
    public class Program
    {
        public static void Main(string[] args){
            Map map = new Map();
            DFS dfs = new DFS();

            map.ReadFile();
            map.displayMap();
            Console.WriteLine("Row: " + map.getRow());
            Console.WriteLine("Col: " + map.getCol());
            Console.WriteLine("Treasure Amount: " + map.getnTreasure());
            Console.WriteLine("Starting Location: " + map.getCurLoc().getRow() + "," + map.getCurLoc().getCol());

            dfs.solve(map);
            Console.WriteLine("Execution Time: " + dfs.getExecutionTime() + " ms");
            Console.WriteLine("Total Nodes: "+ dfs.getNodes());
            Console.WriteLine("Total Steps: "+ dfs.getSteps());
            Console.Write("Solution Route: ");
            dfs.displaySolutionRoutes();
            Console.WriteLine("Solution Paths: ");
            dfs.displaySolutionPaths();
            


            
        }
    }
}
