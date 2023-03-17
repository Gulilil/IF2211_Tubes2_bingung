using System;

namespace src
{
    public class Program
    {
        public static void Main(string[] args){
            Map map = new Map(3,4,5,3,3);
            map.changeCurLoc('D');
            Console.WriteLine(map.getCurLoc().getRow());
            Console.WriteLine("Hello World!");  
            // char[,,] charMatrix = map.ReadFile();
        }
    }
}
