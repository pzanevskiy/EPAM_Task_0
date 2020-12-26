using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextParser.Models;
using TextParser.Models.Interfaces;
using TextParser.Service.Interfaces;

namespace TextParser.Service
{
    public class FileService : IFileService
    {
        public StreamReader GetReader(string filename)
        {
            return new StreamReader(filename);
        }

        public void Write(IText text)
        {
            using(StreamWriter writer=new StreamWriter("Answer.txt", false))
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
