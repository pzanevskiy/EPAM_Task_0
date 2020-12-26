using System;
using System.IO;
using TextParser.Models;
using TextParser.Service;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace TextParser
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Word word = new Word("hello");                  
            Parser parser = new Parser();
            Text text = new Text();
            FileService fileService = new FileService();

            text = parser.ParseText(fileService.GetReader("C:\\Users\\Павел\\source\\repos\\EPAM_Task_0\\TextParser\\Resources\\Text.txt"));

            TextServices textServices = new TextServices();
            //var temp = textServices.getInterrogativeSentencesWordsWithLength(text.Sentences,Convert.ToInt32(Console.ReadLine()));           
            //foreach(var item in temp)
            //{
            //    Console.WriteLine(item);
            //}
            textServices.RemoveWords(text.Sentences);
            text.Sentences=textServices.SortSentences(text.Sentences);
            fileService.Write(text);
            //Hel, lo... Its: me.This is? !text pa; rser!
            Console.WriteLine(text.ToString());         
            
        }
    }
}
