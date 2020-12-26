﻿using System;
using System.IO;
using TextParser.Models;
using TextParser.Service;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using TextParser.Models.Interfaces;

namespace TextParser
{
    class Program
    {
        static void Main(string[] args)
        {                               
            Parser parser = new Parser();
            IText text = new Text();
            FileService fileService = new FileService();

            text = parser.ParseText(fileService.GetReader("C:\\Users\\Павел\\source\\repos\\EPAM_Task_0\\TextParser\\Resources\\Text.txt"));

            TextService textService = new TextService();
            //var temp = textService.GetInterrogativeSentencesWordsWithLength(text.Sentences, Convert.ToInt32(Console.ReadLine()));
            //foreach (var item in temp)
            //{
            //    Console.WriteLine(item);
            //}
            foreach (var item in text.Sentences)
                textService.ReplaceWords(item, 2, "helloWorld");
            //text.Sentences=textServices.RemoveWordsStartsWithConsonants(text.Sentences);
            //text.Sentences=textServices.SortSentences(text.Sentences);
            fileService.Write(text);
            //Hel, lo... Its: me.This is? !text pa; rser!
            Console.WriteLine(text.ToString());         
            
        }
    }
}
