using System;
using System.IO;
namespace inputoutput
{
    public class ReadingFile
    {
        public static int[2] IdentifyFile(string textFile)
        {
            int[2] data = {0,0};
            string[] lines = File.ReadAllLines(textFile);
            foreach (string line in lines);
            Console.WriteLine(lines);

            return data;
        }

        public static char[,,] ReadFile()
        {
            Console.WriteLine("Enter File Name: ");
            string fileName = Console.ReadLine();
            string textFile = string.Concat("../test/", fileName);
            int[2] data = IdentifyFile(textFile);

            char[,,] charMatrix = { {'o','o'}, {'o','o'}};
            return charMatrix;
            
        }
    }
}