using System;

namespace src
{
    public class Program
    {
        public static void Main(string[] args){
            Map map = new Map();
            map.ReadFile();
            map.getInfo();

            Console.WriteLine("========================== BFS");
            BFS bfs = new BFS();
            bfs.getSolution(map);
            bfs.getInfo(false);
            map.resetMap();

            Console.WriteLine("========================== BFS TSP");
            BFS bfs2 = new BFS();
            bfs2.setTSP(true);
            bfs2.getSolution(map);
            bfs2.getInfo(false);
            map.resetMap();

            Console.WriteLine("========================== DFS");
            DFS dfs = new DFS();
            dfs.getSolution(map);
            dfs.getInfo(false);
            map.resetMap();

            Console.WriteLine("========================== DFS TSP");
            DFS dfs2 = new DFS();
            dfs2.setTSP(true);
            dfs2.getSolution(map);
            dfs2.getInfo(false);

            Console.WriteLine("==========================");
            Console.WriteLine("==========================");
            

        }

    }
}
