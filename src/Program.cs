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

            map.resetMap();
            BFS bfs2 = new BFS();
            bfs2.setTSP(true);
            bfs2.getSolution(map);
            bfs2.getInfo();

            map.resetMap();
            DFS dfs = new DFS();
            dfs.getSolution(map);
            dfs.getInfo();

            map.resetMap();
            DFS dfs2 = new DFS();
            dfs2.setTSP(true);
            dfs2.getSolution(map);
            dfs2.getInfo();

            Console.WriteLine("==========================");
            Console.WriteLine("==========================");
            

        }

    }
}
