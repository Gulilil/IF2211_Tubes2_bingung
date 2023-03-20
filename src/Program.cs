using System;

namespace src
{
    public class Program
    {
        public static void Main(string[] args){
            Map map = new Map();
            DFS dfs = new DFS();
            BFS bfs = new BFS();

            map.ReadFile();
            map.getInfo();

            bfs.getSolution(map);
            bfs.getInfo();
            dfs.getSolution(map);
            dfs.getInfo();

            Console.WriteLine("==========================");
            Console.WriteLine("==========================");
            

        }

    }
}
