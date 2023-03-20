using System;

namespace src
{
    public class Program
    {
        public static void Main(string[] args){
            Map map = new Map();
            DFS dfs = new DFS();
            BFS bfs = new BFS();
            Route route = new Route();
            Stack<Point> stackPath = new Stack<Point> ();

            map.ReadFile();
            map.getInfo();

            // dfs.getSolution(route, map, stackPath);
            // dfs.getInfo();
            bfs.setTSP(true);
            bfs.getSolution(map);
            bfs.getInfo();

            Console.WriteLine("==========================");
            Console.WriteLine("==========================");
            

        }

    }
}
