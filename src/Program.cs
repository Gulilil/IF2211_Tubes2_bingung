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
            dfs.getInfo();

            Console.WriteLine("==========================");
            Console.WriteLine("==========================");
            

        }

    }
}
