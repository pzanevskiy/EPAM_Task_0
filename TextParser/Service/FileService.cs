using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextParser.Models;

namespace TextParser.Service
{
    public class FileService
    {
        public StreamReader GetReader(string filename)
        {
            return new StreamReader(filename);
        }

        public void Write(Text text)
        {
            using(StreamWriter writer=new StreamWriter("C:\\Users\\Павел\\source\\repos\\EPAM_Task_0\\TextParser\\Resources\\Answer.txt", false))
            {
                foreach(var sentence in text.Sentences)
                {
                    writer.WriteLine(sentence);
                }
                writer.Close();
            }
        }
    }
}
