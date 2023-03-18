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
            dfs.displayPath(dfs.getCurPath());
        }
    }
}
