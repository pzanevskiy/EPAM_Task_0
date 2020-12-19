using System;
using TextParser.Models;

namespace TextParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Word word = new Word("hello");
            Sentence sentence = new Sentence("Hello,\t its                   me ");
            sentence.SentenceItems.Add(word);
            Console.WriteLine(sentence.ToString());
        }
    }
}
