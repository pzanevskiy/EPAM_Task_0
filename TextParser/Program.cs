using System;
using TextParser.Models;
using TextParser.Service;

namespace TextParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Word word = new Word("hello");                  
            Parser parser = new Parser();
            parser.ParseText("Hel, lo... Its: me. This is?! text pa; rser!");
            Console.WriteLine();           
        }
    }
}
