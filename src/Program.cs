using System;

namespace src
{
    public class Program
    {
        public static void Main(string[] args){
            Map map = new Map();
            map.ReadFile();
            map.getInfo();

            BFS bfs = new BFS();
            bfs.getSolution(map);
            bfs.getInfo();


            map.ReadFile();
            DFS dfs = new DFS();
            dfs.getSolution(map);
            dfs.getInfo();

            Console.WriteLine("==========================");
            Console.WriteLine("==========================");
            

        }

    }
}
