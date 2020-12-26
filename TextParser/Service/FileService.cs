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
        public StreamReader GetReader(string filename) => new StreamReader(filename);

        public void Write(IText text,string filename)
        {
            using(StreamWriter writer=new StreamWriter(filename, false))
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
